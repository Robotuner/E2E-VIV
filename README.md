# E2E-VIV Initiative
**Visual Studio 2019 Community Version 16.8.5**
E2E-VIV is released under GPL, you are free to use, modify and redistribute the code.

The E2E-VIV Initiative is born from the uncertainty of the 2020 Election.  The projects in this initiative represent source code used to mimic the End 2 End process as envisioned during the feasibility investigation.  If inclined, I would like help on all aspects of this initiative, Architect, DB, Testing, or any other input/feedback.  The goal is to fortify what I have to a point where the hacking community can be invited to test and find weaknesses and points of attack.  States will not implement internet voting without certification.  It is doubtful that the US EAC (US Election Assistance Commission) would even know how or what aspects should be certified for a reliable E2E VIV system.  The experience gained in this initiative would probably be of great value when that day arrives.  

The website for this initiative is <a href="https://www.e2e-viv.org">E2E-VIV.org</a>

This initiative is important to me.  I believe it is important for this country.  Please support this effort by making a donation to support this effort.  https://www.paypal.com/donate?hosted_button_id=L25FJ3YJ8C8Q4 

## How I am using the code
I have not set-up website for the API, basically I am:
1. Loading the Election API project as an VS Instance and running it as localhost.
2. Starting NGROK and pointing it to local host.  The command line I am using is:  NGROK http https:/localhost:#### -host-header="localhost:####"
3. I am setting the ElectionModels.Misc.Utils.ElectionUrl = "https:ngrokurlhere/api" then rebuilding both the Election and One Vote projects.
4. In VS Instance2 I am running the Election Project QRScanCode view to create QRScan codes to load into the voting app.
5. In a third VS Instance I am running either the iOS or Android OneVote app.

To simulate a ballot, I run the app, scan the QRScan code in instance 2 then casting votes, taking the headshot and submitting the completed ballot.

## Election Project
Election is a WPF project that has multiple views.  The Primary View is a **QR Scan Code** view that would create a QR Scan code similar to one provided by the State Registration Website.  The Scan code is used by the voting app (One Vote Xamarin Forms Project) to get a ballot. 
QR Scan Code requirements:
- ElectionId
- Registrant Name (debugging, not used anywhere else)
- Birth Year (used if required during audit)
- Encrypted Ballot Guid (using SSN)

**Face Detection** is a view that looks at/tests the OpenCV Library to see how it could be used for Face detection and Face Landmarks.

**Election Ballot** is a view that Gets a Ballot or allows the creation of a ballot.  It is modeled after the Washington State 2020 Ballot information.  The view is not something that would be required by the E2E-VIV source code.  It is more like what would be needed by each state to create ballots.

**Election Summary** View is just a simple UI to look at the distribution of votes among the candidates.  Basically to see what current totals are for each candidate in the test database.

**Election Signature** View looks at signatures for the purpose of comparing head shots.

## PostgresDB
Development has been based on the SQL server DB project.  Decided that this is open source, so it would be better to go with PostgresDB.  Never used Postgres 
before, so it probably could be implemented better.  The script names are self explanatory.  It includes two scripts that will seed the database with
the 2020 Washington State ballot slate.

## Election API Project
This is a .net core api project that exposes the endpoints used by the **Election** and **One Vote** Projects.

## ElectionDB Project
This is a SQL Server Database Project used to create the database, used by the ElectionAPI endpoints.  It preloads the Washington State Ballot, defined triggers.  The stored procedures are not included with the deployment of the database during publishing.  After the database is deployed, you will need to load the stored procedure using SSMS.

## ElectionModels Project
Common objects used by both the **Election** and **One Vote** projects.

## OneVote Project
Xamarin Forms app for both Android and iOS.  Represents the voting app the registered voters will use to cast votes.  This is rather fluid.  Mostly it is subject to what I think the UI should do, not feedback from any States hopefully will eventually choose to implement internet voting.  In particular, I have been attempting to use open source software (OpenCV) for face detection and image capturing based on user action (blinking).  I haven't been very succesful at it yet.  My preference is to do this using open source software rather than use either Google or Microsoft libraries which require licensing and fees.  If there are 150 million ballots, there are 150 million Head shots.  I don't know what it would cost to validate each head shot as unique.  It might not be a small number.

The head shot speaks to the auditing capability of the E2E system, not the ability to tamper or inject ballots into the system.  The ability to do face detection, blinking and facial landmarks speaks to the audit capabilities not necessarily the security of internet voting.
