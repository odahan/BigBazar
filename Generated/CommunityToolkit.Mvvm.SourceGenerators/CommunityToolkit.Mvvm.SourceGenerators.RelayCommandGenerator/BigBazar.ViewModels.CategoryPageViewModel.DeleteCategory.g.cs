﻿// <auto-generated/>
#pragma warning disable
#nullable enable
namespace BigBazar.ViewModels
{
    /// <inheritdoc/>
    partial class CategoryPageViewModel
    {
        /// <summary>The backing field for <see cref="DeleteCategoryCommand"/>.</summary>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.2.0.0")]
        private global::CommunityToolkit.Mvvm.Input.AsyncRelayCommand? deleteCategoryCommand;
        /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand"/> instance wrapping <see cref="DeleteCategory"/>.</summary>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand DeleteCategoryCommand => deleteCategoryCommand ??= new global::CommunityToolkit.Mvvm.Input.AsyncRelayCommand(new global::System.Func<global::System.Threading.Tasks.Task>(DeleteCategory));
    }
}