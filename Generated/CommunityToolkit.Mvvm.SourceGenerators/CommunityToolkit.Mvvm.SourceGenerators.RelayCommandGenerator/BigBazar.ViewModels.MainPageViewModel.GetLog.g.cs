﻿// <auto-generated/>
#pragma warning disable
#nullable enable
namespace BigBazar.ViewModels
{
    /// <inheritdoc/>
    partial class MainPageViewModel
    {
        /// <summary>The backing field for <see cref="GetLogCommand"/>.</summary>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.2.0.0")]
        private global::CommunityToolkit.Mvvm.Input.AsyncRelayCommand? getLogCommand;
        /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand"/> instance wrapping <see cref="GetLog"/>.</summary>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand GetLogCommand => getLogCommand ??= new global::CommunityToolkit.Mvvm.Input.AsyncRelayCommand(new global::System.Func<global::System.Threading.Tasks.Task>(GetLog));
    }
}