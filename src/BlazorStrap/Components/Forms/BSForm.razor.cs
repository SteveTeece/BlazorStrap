﻿using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Forms;
using System.Timers;

namespace BlazorStrap
{
    public class BSForm : EditForm
    {

        protected string classname =>
        new CssBuilder()
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsInline { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected bool ValidateOnInit { get; set; }
        private bool First = true;
        private RenderFragment Form { get; set; }
        private EditContext MyEditContext { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Form = Formbuilder =>
            {
                Formbuilder.OpenComponent<EditForm>(0);
                Formbuilder.AddMultipleAttributes(1, AdditionalAttributes);
                Formbuilder.AddAttribute(2, "class", classname);
                Formbuilder.AddAttribute(3, "Model", Model);
                Formbuilder.AddAttribute(4, "OnSubmit", OnSubmit);
                Formbuilder.AddAttribute(5, "OnValidSubmit", OnValidSubmit);
                Formbuilder.AddAttribute(6, "OnInvalidSubmit", OnInvalidSubmit);
                Formbuilder.AddAttribute(7, "ChildContent", ChildContent);
                Formbuilder.CloseComponent();
            };

            builder.OpenComponent<CascadingValue<BSForm>>(3);
            builder.AddAttribute(4, "IsFixed", true);
            builder.AddAttribute(5, "Value", this);
            builder.AddAttribute(6, RenderTreeBuilder.ChildContent, Form);
            builder.CloseComponent();

        }

        public void FormIsReady(EditContext e)
        {
            MyEditContext = e;
            if (ValidateOnInit)
            {
                ForceValidate();
            }
        }
        public void ForceValidate()
        {
            Invoke(() => MyEditContext?.Validate());
                StateHasChanged();
        }
    }
}