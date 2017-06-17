using System;
using BikeSpot;
using BikeSpot.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomListview), typeof(MyListviewRenderer))]
namespace BikeSpot.iOS
{
public class MyListviewRenderer : ListViewRenderer
{
	protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
	{
		base.OnElementChanged(e);

		if (Control == null)
			return;

		var tableView = Control as UITableView;
		tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			//tableView.ScrollEnabled = false;

			tableView.Scrolled+= TableView_Scrolled;

        }

		void TableView_Scrolled(object sender, EventArgs e)
		{

		}
	}
}