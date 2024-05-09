﻿// <auto-generated/>
#pragma warning disable
#nullable enable
namespace BigBazar.Views
{
    /// <inheritdoc/>
    partial class AboutPage
    {
        /// <inheritdoc cref="version"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public string Version
        {
            get => version;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("version")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<string>.Default.Equals(version, value))
                {
                    OnVersionChanging(value);
                    OnVersionChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.Version);
                    version = value;
                    OnVersionChanged(value);
                    OnVersionChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Version);
                }
            }
        }

        /// <summary>Executes the logic for when <see cref="Version"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Version"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnVersionChanging(string value);
        /// <summary>Executes the logic for when <see cref="Version"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Version"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnVersionChanging(string? oldValue, string newValue);
        /// <summary>Executes the logic for when <see cref="Version"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Version"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnVersionChanged(string value);
        /// <summary>Executes the logic for when <see cref="Version"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Version"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnVersionChanged(string? oldValue, string newValue);
    }
}