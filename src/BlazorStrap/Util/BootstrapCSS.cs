﻿using System;
using System.Threading.Tasks;

namespace BlazorStrap.Util
{
    class BootstrapCSS : IBootstrapCSS
    {
        private readonly BlazorStrapInterop _blazorStrapInterop;
        private readonly CurrentTheme _currentTheme;

        public BootstrapCSS(BlazorStrapInterop blazorStrapInterop, CurrentTheme currentTheme)
        {
            _blazorStrapInterop = blazorStrapInterop;
            _currentTheme = currentTheme;
        }
        public Task SetBootstrapCSS()
        {
            _currentTheme.Theme = Theme.Bootstrap;
            return SetBootstrapCSS("bootstrap", "latest");
        }

        public Task SetBootstrapCSS(string version)
        {
            _currentTheme.Theme = Theme.Bootstrap;
            return SetBootstrapCSS("bootstrap", version);
        }

        public async Task SetBootstrapCSS(string theme, string version)
        {
            var enumTheme = (Theme)Enum.Parse(typeof(Theme), theme.FirstCharToUpper());
            _currentTheme.Theme = enumTheme;
            await _blazorStrapInterop.SetBootstrapCSS(theme.ToLowerInvariant(), version);
        }

        public Task SetBootstrapCSS(Theme theme, string version)
        {
            _currentTheme.Theme = theme;
            return SetBootstrapCSS(theme.ToString().ToLowerInvariant(), version);
        }

        public Theme CurrentTheme => _currentTheme.Theme;
    }
}
