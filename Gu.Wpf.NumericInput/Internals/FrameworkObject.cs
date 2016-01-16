namespace Gu.Wpf.NumericInput
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;

    /// <summary>
    /// http://referencesource.microsoft.com/#PresentationFramework/src/Framework/MS/Internal/FrameworkObject.cs,301f142557ad0322
    /// </summary>
    internal struct FrameworkObject
    {
        private static readonly DependencyObjectType FrameworkElement_DType = DependencyObjectType.FromSystemType(typeof(FrameworkElement));
        private static readonly DependencyObjectType FrameworkContentElement_DType = DependencyObjectType.FromSystemType(typeof(FrameworkContentElement));

        internal FrameworkObject(DependencyObject d)
        {
            // [code should be identical to Reset(d)]
            this._do = d;
            if (FrameworkElement_DType.IsInstanceOfType(d))
            {
                this._fe = (FrameworkElement)d;
                this._fce = null;
            }
            else if (FrameworkContentElement_DType.IsInstanceOfType(d))
            {
                this._fe = null;
                this._fce = (FrameworkContentElement)d;
            }
            else
            {
                this._fe = null;
                this._fce = null;
            }
        }

        //internal FrameworkObject(DependencyObject d, bool throwIfNeither)
        //    : this(d)
        //{
        //    if (throwIfNeither && this._fe == null && this._fce == null)
        //    {
        //        object arg = (d != null) ? (object)d.GetType() : (object)"NULL";
        //        throw new InvalidOperationException(System.Windows.SR.Get(SRID.MustBeFrameworkDerived, arg));
        //    }
        //}

        internal FrameworkObject(FrameworkElement fe, FrameworkContentElement fce)
        {
            this._fe = fe;
            this._fce = fce;

            if (fe != null)
            {
                this._do = fe;
            }
            else
            {
                this._do = fce;
            }
        }

        internal void Reset(DependencyObject d)
        {
            this._do = d;

            if (FrameworkElement_DType.IsInstanceOfType(d))
            {
                this._fe = (FrameworkElement)d;
                this._fce = null;
            }
            else if (FrameworkContentElement_DType.IsInstanceOfType(d))
            {
                this._fe = null;
                this._fce = (FrameworkContentElement)d;
            }
            else
            {
                this._fe = null;
                this._fce = null;
            }
        }

        internal FrameworkElement FE => this._fe;

        internal FrameworkContentElement FCE => this._fce;

        internal DependencyObject DO => this._do;

        internal bool IsFE => this._fe != null;

        internal bool IsFCE => this._fce != null;

        internal bool IsValid => this._fe != null || this._fce != null;

        //// logical parent
        //internal DependencyObject Parent
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.Parent;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.Parent;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //internal int TemplateChildIndex
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.TemplateChildIndex;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.TemplateChildIndex;
        //        }
        //        else
        //        {
        //            return -1;
        //        }
        //    }
        //}

        //internal DependencyObject TemplatedParent
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.TemplatedParent;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.TemplatedParent;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //internal Style ThemeStyle
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.ThemeStyle;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.ThemeStyle;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //internal XmlLanguage Language
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.Language;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.Language;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //internal FrameworkTemplate TemplateInternal
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.TemplateInternal;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //internal FrameworkObject FrameworkParent
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            DependencyObject parent = this._fe.ContextVerifiedGetParent();

        //            // NOTE: Logical parent can only be an FE, FCE
        //            if (parent != null)
        //            {
        //                Invariant.Assert(parent is FrameworkElement || parent is FrameworkContentElement);

        //                if (this._fe.IsParentAnFE)
        //                {
        //                    return new FrameworkObject((FrameworkElement)parent, null);
        //                }
        //                else
        //                {
        //                    return new FrameworkObject(null, (FrameworkContentElement)parent);
        //                }
        //            }

        //            // This is when current does not have a logical parent that is an fe or fce
        //            FrameworkObject foParent = GetContainingFrameworkElement(this._fe.InternalVisualParent);
        //            if (foParent.IsValid)
        //            {
        //                return foParent;
        //            }

        //            // allow subclasses to override (e.g. Popup)
        //            foParent.Reset(this._fe.GetUIParentCore());
        //            if (foParent.IsValid)
        //            {
        //                return foParent;
        //            }

        //            // try InheritanceContext
        //            foParent.Reset(Helper.FindMentor(this._fe.InheritanceContext));
        //            return foParent;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            DependencyObject parent = this._fce.Parent;

        //            // NOTE: Logical parent can only be an FE, FCE
        //            if (parent != null)
        //            {
        //                Invariant.Assert(parent is FrameworkElement || parent is FrameworkContentElement);

        //                if (this._fce.IsParentAnFE)
        //                {
        //                    return new FrameworkObject((FrameworkElement)parent, null);
        //                }
        //                else
        //                {
        //                    return new FrameworkObject(null, (FrameworkContentElement)parent);
        //                }
        //            }

        //            // This is when current does not have a logical parent that is an fe or fce
        //            parent = ContentOperations.GetParent((ContentElement)this._fce);
        //            FrameworkObject foParent = GetContainingFrameworkElement(parent);
        //            if (foParent.IsValid)
        //            {
        //                return foParent;
        //            }

        //            // try InheritanceContext
        //            foParent.Reset(Helper.FindMentor(this._fce.InheritanceContext));
        //            return foParent;
        //        }
        //        else
        //        {
        //            return GetContainingFrameworkElement(this._do);
        //        }
        //    }
        //}

        internal static FrameworkObject GetContainingFrameworkElement(DependencyObject current)
        {
            FrameworkObject fo = new FrameworkObject(current);

            while (!fo.IsValid && fo.DO != null)
            {
                // The current object is neither a FrameworkElement nor a
                // FrameworkContentElement.  We will now walk the "core"
                // tree looking for one.
                Visual visual;
                Visual3D visual3D;
                ContentElement ce;

                if ((visual = fo.DO as Visual) != null)
                {
                    fo.Reset(VisualTreeHelper.GetParent(visual));
                }
                else if ((ce = fo.DO as ContentElement) != null)
                {
                    fo.Reset(ContentOperations.GetParent(ce));
                }
                else if ((visual3D = fo.DO as Visual3D) != null)
                {
                    fo.Reset(VisualTreeHelper.GetParent(visual3D));
                }
                else
                {
                    // The parent could be an application.
                    fo.Reset(null);
                }
            }

            return fo;
        }

        //// Style property
        //internal Style Style
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.Style;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.Style;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.Style = value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.Style = value;
        //        }
        //    }
        //}

        // IsStyleSetFromGenerator property
        //internal bool IsStyleSetFromGenerator
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.IsStyleSetFromGenerator;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.IsStyleSetFromGenerator;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.IsStyleSetFromGenerator = value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.IsStyleSetFromGenerator = value;
        //        }
        //    }
        //}

        // returns the effective parent, whether visual, logical,
        // inheritance context, etc.
        internal DependencyObject EffectiveParent
        {
            get
            {
                DependencyObject parent;

                if (this.IsFE)
                {
                    parent = VisualTreeHelper.GetParent(this._fe);
                }
                else if (this.IsFCE)
                {
                    parent = this._fce.Parent;
                }
                else
                {
                    Visual visual;
                    Visual3D visual3D;
                    ContentElement ce;

                    if ((visual = this._do as Visual) != null)
                    {
                        parent = VisualTreeHelper.GetParent(visual);
                    }
                    else if ((ce = this._do as ContentElement) != null)
                    {
                        parent = ContentOperations.GetParent(ce);
                    }
                    else if ((visual3D = this._do as Visual3D) != null)
                    {
                        parent = VisualTreeHelper.GetParent(visual3D);
                    }
                    else
                    {
                        parent = null;
                    }
                }

                if (parent == null && this._do != null)
                {
                    throw new NotImplementedException();
                    //parent = this._do.InheritanceContext;
                }

                return parent;
            }
        }

        //internal FrameworkObject PreferVisualParent => this.GetPreferVisualParent(false);

        //internal bool IsLoaded
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.IsLoaded;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.IsLoaded;
        //        }
        //        else
        //        {
        //            return BroadcastEventHelper.IsParentLoaded(this._do);
        //        }
        //    }
        //}

        //internal bool IsInitialized
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.IsInitialized;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.IsInitialized;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //}

        //internal bool ThisHasLoadedChangeEventHandler
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.ThisHasLoadedChangeEventHandler;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.ThisHasLoadedChangeEventHandler;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        //internal bool SubtreeHasLoadedChangeHandler
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.SubtreeHasLoadedChangeHandler;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.SubtreeHasLoadedChangeHandler;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.SubtreeHasLoadedChangeHandler = value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.SubtreeHasLoadedChangeHandler = value;
        //        }
        //    }
        //}

        //internal InheritanceBehavior InheritanceBehavior
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.InheritanceBehavior;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.InheritanceBehavior;
        //        }
        //        else
        //        {
        //            return InheritanceBehavior.Default;
        //        }
        //    }
        //}

        //internal bool StoresParentTemplateValues
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.StoresParentTemplateValues;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.StoresParentTemplateValues;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.StoresParentTemplateValues = value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.StoresParentTemplateValues = value;
        //        }
        //    }
        //}

        //internal bool HasResourceReference
        //{
        //    /* not used (yet)
        //    get
        //    {
        //        if (IsFE)
        //        {
        //            return _fe.HasResourceReference;
        //        }
        //        else if (IsFCE)
        //        {
        //            return _fce.HasResourceReference;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    */
        //    set
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.HasResourceReference = value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.HasResourceReference = value;
        //        }
        //    }
        //}

        /* not used (yet)
        internal bool HasStyleChanged
        {
            get
            {
                if (IsFE)
                {
                    return _fe.HasStyleChanged;
                }
                else if (IsFCE)
                {
                    return _fce.HasStyleChanged;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (IsFE)
                {
                    _fe.HasStyleChanged = value;
                }
                else if (IsFCE)
                {
                    _fce.HasStyleChanged = value;
                }
            }
        }
        */

        //internal bool HasTemplateChanged
        //{
        //    /* not used (yet)
        //    get
        //    {
        //        if (IsFE)
        //        {
        //            return _fe.HasTemplateChanged;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    */
        //    set
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.HasTemplateChanged = value;
        //        }
        //    }
        //}

        // Says if there are any implicit styles in the ancestry
        //internal bool ShouldLookupImplicitStyles
        //{
        //    get
        //    {
        //        if (this.IsFE)
        //        {
        //            return this._fe.ShouldLookupImplicitStyles;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            return this._fce.ShouldLookupImplicitStyles;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.ShouldLookupImplicitStyles = value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.ShouldLookupImplicitStyles = value;
        //        }
        //    }
        //}

        internal static bool IsEffectiveAncestor(DependencyObject d1, DependencyObject d2)
        {
            for (FrameworkObject fo = new FrameworkObject(d2);
                fo.DO != null;
                fo.Reset(fo.EffectiveParent))
            {
                if (ReferenceEquals(fo.DO, d1))
                {
                    return true;
                }
            }

            return false;
        }

#if TRACE_INHERITANCE_CONTEXT
        static StreamWriter logFile;
        internal static void Log(string format, params object[] args)
        {
            if (logFile == null)
            {
                logFile = File.AppendText("IClog.txt");
                logFile.WriteLine();
                logFile.WriteLine("Log for {0}", Environment.CommandLine);
            }

            logFile.WriteLine(format, args);
            logFile.Flush();
        }

        internal static string LogIC(DependencyObject d1, DependencyProperty dp, DependencyObject d2)
        {
            string name = (dp == null) ? "[null]" : dp.Name;
            return String.Format("{0}.{1} = {2}", d1.GetType().Name, name, d2.GetType().Name);
        }
#endif

        //internal void ChangeLogicalParent(DependencyObject newParent)
        //{
        //    if (this.IsFE)
        //    {
        //        this._fe.ChangeLogicalParent(newParent);
        //    }
        //    else if (this.IsFCE)
        //    {
        //        this._fce.ChangeLogicalParent(newParent);
        //    }
        //}

        //internal void BeginInit()
        //{
        //    if (this.IsFE)
        //    {
        //        this._fe.BeginInit();
        //    }
        //    else if (this.IsFCE)
        //    {
        //        this._fce.BeginInit();
        //    }
        //    else
        //    {
        //        this.UnexpectedCall();
        //    }
        //}

        //internal void EndInit()
        //{
        //    if (this.IsFE)
        //    {
        //        this._fe.EndInit();
        //    }
        //    else if (this.IsFCE)
        //    {
        //        this._fce.EndInit();
        //    }
        //    else
        //    {
        //        this.UnexpectedCall();
        //    }
        //}

        //internal object FindName(string name, out DependencyObject scopeOwner)
        //{
        //    if (this.IsFE)
        //    {
        //        return this._fe.FindName(name, out scopeOwner);
        //    }
        //    else if (this.IsFCE)
        //    {
        //        return this._fce.FindName(name, out scopeOwner);
        //    }
        //    else
        //    {
        //        scopeOwner = null;
        //        return null;
        //    }
        //}

        //// returns the parent in the "prefer-visual" sense.
        //// That is, this method
        ////  1. prefers visual to logical parent (with InheritanceContext last)
        ////  2. does not see parents whose InheritanceBehavior forbids it
        //// Call with force=true to get the parent even if the current object doesn't
        //// allow it via rule 2.
        //internal FrameworkObject GetPreferVisualParent(bool force)
        //{
        //    // If we're not allowed to move up from here, return null
        //    InheritanceBehavior inheritanceBehavior = force ? InheritanceBehavior.Default : this.InheritanceBehavior;
        //    if (inheritanceBehavior != InheritanceBehavior.Default)
        //    {
        //        return new FrameworkObject(null);
        //    }

        //    FrameworkObject parent = this.GetRawPreferVisualParent();

        //    // make sure the parent allows itself to be found
        //    switch (parent.InheritanceBehavior)
        //    {
        //        case InheritanceBehavior.SkipToAppNow:
        //        case InheritanceBehavior.SkipToThemeNow:
        //        case InheritanceBehavior.SkipAllNow:
        //            parent.Reset(null);
        //            break;

        //        default:
        //            break;
        //    }

        //    return parent;
        //}

        //// helper used by GetPreferVisualParent - doesn't check InheritanceBehavior
        //private FrameworkObject GetRawPreferVisualParent()
        //{
        //    // the null object has no parent
        //    if (this._do == null)
        //    {
        //        return new FrameworkObject(null);
        //    }

        //    // get visual parent
        //    DependencyObject visualParent;
        //    if (this.IsFE)
        //    {
        //        visualParent = VisualTreeHelper.GetParent(this._fe);
        //    }
        //    else if (this.IsFCE)
        //    {
        //        visualParent = null;
        //    }
        //    else if (this._do != null)
        //    {
        //        Visual visual = this._do as Visual;
        //        visualParent = (visual != null) ? VisualTreeHelper.GetParent(visual) : null;
        //    }
        //    else
        //    {
        //        visualParent = null;
        //    }

        //    if (visualParent != null)
        //    {
        //        return new FrameworkObject(visualParent);
        //    }

        //    // if no visual parent, get logical parent
        //    DependencyObject logicalParent;
        //    if (this.IsFE)
        //    {
        //        logicalParent = this._fe.Parent;
        //    }
        //    else if (this.IsFCE)
        //    {
        //        logicalParent = this._fce.Parent;
        //    }
        //    else if (this._do != null)
        //    {
        //        ContentElement ce = this._do as ContentElement;
        //        logicalParent = (ce != null) ? ContentOperations.GetParent(ce) : null;
        //    }
        //    else
        //    {
        //        logicalParent = null;
        //    }

        //    if (logicalParent != null)
        //    {
        //        return new FrameworkObject(logicalParent);
        //    }

        //    // if no logical or visual parent, get "uiCore" parent
        //    UIElement uiElement;
        //    ContentElement contentElement;
        //    DependencyObject uiCoreParent;
        //    if ((uiElement = this._do as UIElement) != null)
        //    {
        //        uiCoreParent = uiElement.GetUIParentCore();
        //    }
        //    else if ((contentElement = this._do as ContentElement) != null)
        //    {
        //        uiCoreParent = contentElement.GetUIParentCore();
        //    }
        //    else
        //    {
        //        uiCoreParent = null;
        //    }

        //    if (uiCoreParent != null)
        //    {
        //        return new FrameworkObject(uiCoreParent);
        //    }

        //    // if all else fails, use InheritanceContext
        //    return new FrameworkObject(this._do.InheritanceContext);
        //}

        //internal void RaiseEvent(RoutedEventArgs args)
        //{
        //    if (this.IsFE)
        //    {
        //        this._fe.RaiseEvent(args);
        //    }
        //    else if (this.IsFCE)
        //    {
        //        this._fce.RaiseEvent(args);
        //    }
        //}

        //internal void OnLoaded(RoutedEventArgs args)
        //{
        //    if (this.IsFE)
        //    {
        //        this._fe.OnLoaded(args);
        //    }
        //    else if (this.IsFCE)
        //    {
        //        this._fce.OnLoaded(args);
        //    }
        //}

        //internal void OnUnloaded(RoutedEventArgs args)
        //{
        //    if (this.IsFE)
        //    {
        //        this._fe.OnUnloaded(args);
        //    }
        //    else if (this.IsFCE)
        //    {
        //        this._fce.OnUnloaded(args);
        //    }
        //}

        //internal void ChangeSubtreeHasLoadedChangedHandler(DependencyObject mentor)
        //{
        //    if (this.IsFE)
        //    {
        //        this._fe.ChangeSubtreeHasLoadedChangedHandler(mentor);
        //    }
        //    else if (this.IsFCE)
        //    {
        //        this._fce.ChangeSubtreeHasLoadedChangedHandler(mentor);
        //    }
        //}

        //internal void OnInheritedPropertyChanged(ref InheritablePropertyChangeInfo info)
        //{
        //    if (this.IsFE)
        //    {
        //        this._fe.RaiseInheritedPropertyChangedEvent(ref info);
        //    }
        //    else if (this.IsFCE)
        //    {
        //        this._fce.RaiseInheritedPropertyChangedEvent(ref info);
        //    }
        //}

        //// Set the ShouldLookupImplicitStyles flag on the current
        //// node if the parent has it set to true.
        //internal void SetShouldLookupImplicitStyles()
        //{
        //    if (!this.ShouldLookupImplicitStyles)
        //    {
        //        FrameworkObject parent = this.FrameworkParent;
        //        if (parent.IsValid && parent.ShouldLookupImplicitStyles)
        //        {
        //            this.ShouldLookupImplicitStyles = true;
        //        }
        //    }
        //}

        //internal event RoutedEventHandler Loaded
        //{
        //    add
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.Loaded += value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.Loaded += value;
        //        }
        //        else
        //        {
        //            this.UnexpectedCall();
        //        }
        //    }
        //    remove
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.Loaded -= value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.Loaded -= value;
        //        }
        //        else
        //        {
        //            this.UnexpectedCall();
        //        }
        //    }
        //}

        //internal event RoutedEventHandler Unloaded
        //{
        //    add
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.Unloaded += value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.Unloaded += value;
        //        }
        //        else
        //        {
        //            this.UnexpectedCall();
        //        }
        //    }
        //    remove
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.Unloaded -= value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.Unloaded -= value;
        //        }
        //        else
        //        {
        //            this.UnexpectedCall();
        //        }
        //    }
        //}

        //internal event InheritedPropertyChangedEventHandler InheritedPropertyChanged
        //{
        //    add
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.InheritedPropertyChanged += value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.InheritedPropertyChanged += value;
        //        }
        //        else
        //        {
        //            this.UnexpectedCall();
        //        }
        //    }
        //    remove
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.InheritedPropertyChanged -= value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.InheritedPropertyChanged -= value;
        //        }
        //        else
        //        {
        //            this.UnexpectedCall();
        //        }
        //    }
        //}

        //internal event EventHandler ResourcesChanged
        //{
        //    add
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.ResourcesChanged += value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.ResourcesChanged += value;
        //        }
        //        else
        //        {
        //            this.UnexpectedCall();
        //        }
        //    }
        //    remove
        //    {
        //        if (this.IsFE)
        //        {
        //            this._fe.ResourcesChanged -= value;
        //        }
        //        else if (this.IsFCE)
        //        {
        //            this._fce.ResourcesChanged -= value;
        //        }
        //        else
        //        {
        //            this.UnexpectedCall();
        //        }
        //    }
        //}

        //void UnexpectedCall()
        //{
        //    Invariant.Assert(false, "Call to FrameworkObject expects either FE or FCE");
        //}

        public override string ToString()
        {
            if (this.IsFE)
            {
                return this._fe.ToString();
            }
            else if (this.IsFCE)
            {
                return this._fce.ToString();
            }

            return "Null";
        }

        private FrameworkElement _fe;
        private FrameworkContentElement _fce;
        private DependencyObject _do;
    }
}