#E2E-VIV Initiative
The E2E-VIV Initiative is born from the uncertainty of the 2020 Election.  The projects in this initiative represent source code used to mimic the End 2 End process.
##Election Project
Election is a WPF project that has multiple views.  The Primary View is a **QR Scan Code** view that would create a QR Scan code similar to one provided by the State Registration Website.  The Scan code is used by the voting app (One Vote Xamarin Forms Project) to get a ballot.

**Face Detection** is a view that looks at/tests the OpenCV Library to see how it could be used for Face detection and Face Landmarks.

**Election Ballot** is a view that Gets a Ballot or allows the creation of a ballot.  It is modeled after the Washington State 2020 Ballot information.  The view is not something that would be required by the E2E-VIV source code.  It is more like what would be needed by each state to create ballots.

**Election Summary** View is just a simple UI to look at the distribution of votes among the candidates.  Basically to see what current totals are for each candidate in the test database.

**Election Signature** View looks at signatures for the purpose of comparing head shots.

##Election API Project
This is a .net core api project that exposes the endpoints used by the **Election** and **One Vote** Projects.

##ElectionDB Project
This is a SQL Server Database Project used to create the database, used by the ElectionAPI endpoints.  It preloads the Washington State Ballot, defined triggers.  The stored procedures are not included with the deployment of the database during publishing.  After the database is deployed, you will need to load the stored procedure using SSMS.

##ElectionModels Project
Common objects used by both the **Election** and **One Vote** projects.

## OneVote Project
Xamarin Forms app for both Android and iOS.  Represents the voting app the registered voters will use to cast votes.
