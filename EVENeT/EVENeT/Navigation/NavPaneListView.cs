using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace EVENeT.Navigation
{
    class NavPaneListView : ListView
    {
        private SplitView splitViewHost;

        public NavPaneListView()
        {
            this.SelectionMode = ListViewSelectionMode.Single;
            this.IsItemClickEnabled = true;
            this.ItemClick += ItemClickedHandler;

            // Locate the hosting SplitView control
            this.Loaded += (s, a) =>
            {
                DependencyObject parent = VisualTreeHelper.GetParent(this);
                while (parent != null && !(parent is SplitView))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent != null)
                {
                    this.splitViewHost = parent as SplitView;
                    splitViewHost.RegisterPropertyChangedCallback(SplitView.IsPaneOpenProperty, (sender, args) =>
                    {
                        this.OnPaneToggled();
                    });

                    // Call once to ensure we're in the correct state
                    this.OnPaneToggled();
                }
            };
        }


        private void ItemClickedHandler(object sender, ItemClickEventArgs e)
        {
            // Triggered when the item is selected using something other than a keyboard
            DependencyObject item = this.ContainerFromItem(e.ClickedItem);
            this.InvokeItem(item);
        }

        private void InvokeItem(object focusedItem)
        {
            this.SetSelectedItem(focusedItem as ListViewItem);
            this.ItemInvoked(this, focusedItem as ListViewItem);

            if (this.splitViewHost.IsPaneOpen && (
                this.splitViewHost.DisplayMode == SplitViewDisplayMode.CompactOverlay ||
                this.splitViewHost.DisplayMode == SplitViewDisplayMode.Overlay))
            {
                this.splitViewHost.IsPaneOpen = false;
                if (focusedItem is ListViewItem)
                {
                    ((ListViewItem)focusedItem).Focus(FocusState.Programmatic);
                }
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Remove the entrance animation on the item containers.
            for (int i = 0; i < this.ItemContainerTransitions.Count; i++)
            {
                if (this.ItemContainerTransitions[i] is EntranceThemeTransition)
                {
                    this.ItemContainerTransitions.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Mark the <paramref name="item"/> as selected and ensures everything else is not.
        /// If the <paramref name="item"/> is null then everything is unselected.
        /// </summary>
        /// <param name="item"></param>
        private void SetSelectedItem(ListViewItem item)
        {
            int index = -1;
            if (item != null)
            {
                index = this.IndexFromContainer(item);
            }

            for (int i = 0; i < this.Items.Count; ++i)
            {
                ListViewItem lvi = (ListViewItem)this.ContainerFromIndex(index);
                if (i != index)
                {
                    lvi.IsSelected = false;
                }
                else if (i == index)
                {
                    lvi.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// Occurs when an item has been selected
        /// </summary>
        public event EventHandler<ListViewItem> ItemInvoked;

        /// <summary>
        /// Re-size the ListView's Panel when the SplitView is compact so the items
        /// will fit within the visible space and correctly display a keyboard focus rect.
        /// </summary>
        private void OnPaneToggled()
        {
            if (this.splitViewHost.IsPaneOpen)
            {
                this.ItemsPanelRoot.ClearValue(FrameworkElement.WidthProperty);
                this.ItemsPanelRoot.ClearValue(FrameworkElement.HorizontalAlignmentProperty);
            }
            else if (this.splitViewHost.DisplayMode == SplitViewDisplayMode.CompactInline ||
                this.splitViewHost.DisplayMode == SplitViewDisplayMode.CompactOverlay)
            {
                this.ItemsPanelRoot.SetValue(FrameworkElement.WidthProperty, this.splitViewHost.CompactPaneLength);
                this.ItemsPanelRoot.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            }
        }
    }
}
