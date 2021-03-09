# E2E-VIV
Election Project: WPF Project for pieces of code that being tested of needed to support voting.

Electon API: an API that serves ballots, and accepts signatures.

ElectionDB: SQL Server Database table for Election voting.  

ElectionModels: Intermediate DLL that is shared by the Election Project and the One Vote Xamarin App

ElectionResultAPI: Not used, the initial thought was to separate the API for retrieving the ballot and submitting the ballots into separate API's.  
Currently both are consolidated into the Election API.

ElectionResultDB: Not used, the initial thought was to separate the Signature and Vote table into a separate DB.  
Currently they are both included in the ElectionDB project.

One Vote app: A Xamarin forms app for voting.
