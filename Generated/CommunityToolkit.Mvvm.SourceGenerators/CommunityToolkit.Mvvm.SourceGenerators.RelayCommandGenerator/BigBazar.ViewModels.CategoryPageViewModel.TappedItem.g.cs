﻿// <auto-generated/>
#pragma warning disable
#nullable enable
namespace BigBazar.ViewModels
{
    /// <inheritdoc/>
    partial class CategoryPageViewModel
    {
        /// <summary>The backing field for <see cref="TappedItemCommand"/>.</summary>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.2.0.0")]
        private global::CommunityToolkit.Mvvm.Input.AsyncRelayCommand<global::BigBazar.Models.Category>? tappedItemCommand;
        /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="TappedItem"/>.</summary>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand<global::BigBazar.Models.Category> TappedItemCommand => tappedItemCommand ??= new global::CommunityToolkit.Mvvm.Input.AsyncRelayCommand<global::BigBazar.Models.Category>(new global::System.Func<global::BigBazar.Models.Category, global::System.Threading.Tasks.Task>(TappedItem));
    }
}