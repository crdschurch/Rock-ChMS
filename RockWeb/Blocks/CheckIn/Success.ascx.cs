﻿//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Rock;
using Rock.CheckIn;

namespace RockWeb.Blocks.CheckIn
{
    [Description( "Check-In Success block" )]
    public partial class Success : CheckInBlock
    {
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            if ( CurrentWorkflow == null || CurrentCheckInState == null )
            {
                GoToWelcomePage();
            }
            else
            {
                foreach ( var family in CurrentCheckInState.CheckIn.Families.Where( f => f.Selected ) )
                {
                    lbAnother.Visible = family.People.Count > 1;

                    foreach ( var person in family.People.Where( p => p.Selected ) )
                    {
                        foreach ( var groupType in person.GroupTypes.Where( g => g.Selected ) )
                        {
                            foreach ( var location in groupType.Locations.Where( l => l.Selected ) )
                            {
                                foreach ( var group in location.Groups.Where( g => g.Selected ) )
                                {
                                    foreach ( var schedule in group.Schedules.Where( s => s.Selected ) )
                                    {
                                        var li = new HtmlGenericControl("li");
                                        li.InnerText = string.Format("{0} was checked into {1} for the {2} at {3}",
                                            person.ToString(), group.ToString(), location.ToString(), schedule.ToString(), person.SecurityCode);

                                        phResults.Controls.Add(li);
                                    }
                                }
                            }

                            AddLabelScript( groupType.Labels.Where( l => l.PrintFrom == Rock.Model.PrintFrom.Client).ToJson() );

                        }
                    }
                }
            }
        }

        protected void lbDone_Click( object sender, EventArgs e )
        {
            GoToWelcomePage();
        }

        protected void lbAnother_Click( object sender, EventArgs e )
        {
            if ( KioskCurrentlyActive )
            {
                foreach ( var family in CurrentCheckInState.CheckIn.Families.Where( f => f.Selected ) )
                {
                    foreach ( var person in family.People.Where( p => p.Selected ) )
                    {
                        person.Selected = false;

                        foreach ( var groupType in person.GroupTypes.Where( g => g.Selected ) )
                        {
                            groupType.Selected = false;
                            groupType.Locations = new List<CheckInLocation>();
                        }
                    }
                }

                SaveState();
                GoToPersonSelectPage();

            }
            else
            {
                GoToWelcomePage();
            }
        }

        private void AddLabelScript( string jsonObject )
        {
            string script = string.Format( @"
    // label data
    var labelData = {0};
            
    // setup deviceready event to wait for cordova
	document.addEventListener('deviceready', onDeviceReady, false);

	function onDeviceReady() {{

		// send label data to the zebra plugin for printing
		printLabels();
	           
	}}
		   
	function printLabels() {{
		ZebraPrintPlugin.printTags(
	    JSON.stringify(labelData), 
	    function(result) {{
				console.log('I printed that tag like a champ!!!');
			}},
			function(error) {{   
				// error is an array where:
				// error[0] is the error message
				// error[1] determines if a re-print is possible (in the case where the JSON is good, but the printer was not connected)
				console.log('An error occurred: ' + error[0]);
			}}
	    );
	}}
", jsonObject );
            ScriptManager.RegisterStartupScript( this, this.GetType(), "addLabelScript", script, true );
        }

    }
}