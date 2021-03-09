USE ElectionDB

ALTER table Ticket NOCHECK CONSTRAINT ALL
ALTER table Category NOCHECK CONSTRAINT ALL
GO

declare @electionid UNIQUEIDENTIFIER
SET @electionid = 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6'

declare @catid UNIQUEIDENTIFIER
-- measures
SET @catid = '658F3CDB-FBE2-44EC-B258-6811FA63271C'    --cat1
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Approved', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Rejected', 2)
SET @catid = '3D67CFD2-2645-44EB-8196-02E729875D0B'    -- cat2
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Repealed', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Maintained', 2)
SET @catid = '6AF40813-D42C-4DA0-A8E0-FE61D922C680'   --cat3
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Repealed', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Maintained', 2)
SET @catid = '716C38A8-6CA2-427E-9FEC-763B997F3C54'   --cat4
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Repealed', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Maintained', 2)
SET @catid = '9650C476-C579-4D4D-887D-9C154A95239B'   --cat5
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Repealed', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Maintained', 2)
SET @catid = '83F84069-0DD1-4E13-833B-737E85F64CFF'  --cat6
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Approved', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (NEWID(), @electionid, @catid, 'Rejected', 2)


-- federal
SET @catid = '90545980-C4BC-48D7-BE0A-97730AAD3972'   --cat7
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (NEWID(), @electionid, @catid, 'Joseph R. Biden / Kamala D. Harris', 1, 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (NEWID(), @electionid, @catid, 'Donald J. Trump / Michael R. Pence', 2, 2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (NEWID(), @electionid, @catid, 'Jo Jorgensen / Jeremy "Spike" Cohen', 3, 3)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (NEWID(), @electionid, @catid, 'Howie Hawkins / Angela Walker', 4, 4)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (NEWID(), @electionid, @catid, 'Gloris La Riva / Sunil Freeman', 5, 5)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (NEWID(), @electionid, @catid, 'Alyson Kennedy / Malcolm M. Jarrett', 6, 6)
-- federal congressional disticts
SET @catid = 'D8B38531-E9C0-4B29-A368-4791DA8090F3'   --cat8
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Suzan DelBene', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Jeffrey Beeler, Sr', 2)
SET @catid = '667BD869-D334-48E7-8C20-827B5F6983BC'  --cat9
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Rick Larsen', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Timothy S. Hazelo', 2)
SET @catid = '70EFE57B-FCB0-46C0-8559-230B8D6C301E'  --cat10
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Jaime Herrera Beutler', 2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Carolyn Long', 1)
SET @catid = '0BFF8D66-AA56-4A99-9FFE-DFABF4A0A6C9'  --cat11
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Dan Newhouse', 2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Douglas E. McKinley', 1)
SET @catid = '8DBCD15C-B7BF-4E25-B5FA-EF2B9300FA0D'  --cat12
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Cathy McMorris Rodgers', 2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Dave Wilson', 1)
SET @catid = '29673FC3-CFC9-47D8-87F6-6389E6089FB3'  --cat13
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Derek Kilmer', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Elizabeth Kreiselmaier', 2)
SET @catid = '95B46107-D6BB-4EDF-8D5D-87C648FCE7DB'  --cat14
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Pramila Jayapal', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Craig Keller', 2)
SET @catid = '05B6D9DC-73EE-4E1C-A352-AB6FBF91D208'  --cat15
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Kim Schrier', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Jesse Jensen', 2)
SET @catid = 'BD29F494-E223-47CE-8385-ED98CD096ABE'  --cat16
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Adam Smith', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Doug Basler', 2)
SET @catid = 'DE7F37F2-C9DB-45B6-A5FA-DFFE67EB394A'  --cat17
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Marilyn Strickland', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (NEWID(), @electionid, @catid, 'Beth Doglio', 2)

-- statewide
SET @catid = '383F37D2-4613-4CF2-A83C-7E004251CA2F'  --cat18
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Jay Inslee', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Loren Culp', 2)
SET @catid = '5AD15A1E-593E-45FE-B149-BE43B91FC82B'  --cat19
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Denny Heck', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Marko Liias', 1)
SET @catid = '6383E314-2111-45DE-A0E4-908AB83E97D5'  --cat20
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Kim Wyman', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Gael Tarleton', 2)
SET @catid = '9B4E9F03-E39F-4BB5-8838-F6137EF391C1'  --cat21
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Mike Pellicciotti', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Duane A. Davidson', 2)
SET @catid = 'C33EC949-6CE7-4F5B-A863-B3EE792247EC'  --cat22
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Pat McCarthy', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Chris Leyba', 2)
SET @catid = '3C54BD34-AB8A-4F13-810F-F128DAA8766E'  --cat23
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Bob Ferguson', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Matt Larkin', 2)
SET @catid = '4E796214-2FD1-4177-9F1D-24BA13BAAE43'  --cat24
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Hilary Franz', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Sue Kuehl Pederson', 2)
SET @catid = '3FE4CFD6-526C-4D6A-AF56-E12EAE745ABC'  --cat25
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Chris Reykdal', null)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Maia Espinoza', null)
SET @catid = 'A983A6BB-98C0-44B6-A3CD-4E8E38BBF165'  --cat26
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Mike Kreidler', 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (NEWID(), @electionid, @catid, 'Chirayu Avinash Patel', 2)

--Legislative
SET @catid = 'E5CAB96F-D5C8-4835-9313-FA503E3C44CC'    -- Legislative District 1
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType)values (NEWID(), @electionid, @catid, 'Derek Stanford', 1, 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType)values (NEWID(), @electionid, @catid, 'Art Coday', 2, 1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Davina Duerr', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Adam Bartholomew', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Shelly Kloba', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jeb Brewer', 2,2)
SET @catid = '75AC1826-6092-4E79-B3A6-662AEB557FCB'    -- Legislative District 2
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Rick Payne', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jim McCune', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Andrew Barkis', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'JT Wilcox', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Veronica Whitcher Rockett', 1,2)
SET @catid = 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3'    -- Legislative District 3
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Andy Billig', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Dave Lucas', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Marcus Riccelli', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Laura D Carder', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Timm Ormsby', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bob Apple', 2,2)

SET @catid = '77DA07AA-A491-4426-B4DA-9B9843C754A1'    -- Legislative District 4
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mike Padden', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'John Roskelley', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bob McCaslin', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Lori Feagan', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Lance Gurel', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Rob Chase', 2,2)

SET @catid = '538F1962-C23B-4004-8561-CEAD17639EAA'    -- Legislative District 5
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Ingrid Anderson', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mark Mullet', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bill Ramos', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Ken Moninski', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Lisa Callan', 1,2)

SET @catid = '29313286-0E18-4D70-ACB6-549EF8421B5B'   -- Legislative District 6
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mike Volz', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Zack Zappone', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jenny Graham', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tom McGarry', 1,2)

SET @catid = '7B5026A3-D8AA-4EB2-84DD-E7B4A58199E3'   -- Legislative District 7
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jacquelin Maycumber', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Georgia D. Davenport', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Joel Kretz', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'JJ Wandler', 3,2)

SET @catid = 'A31CC2C5-5AEE-497C-83B4-ADF7AC27306C'   -- Legislative District 8
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Brad Klippert', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Shir Regev', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Matt Boehnke', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Larry Stanley', 2,2)

SET @catid = 'BABE5E86-5342-4A59-A2FD-F13CE8DB3FAA'    -- Legislative District 9 cat35
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mark G. Schoesler', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jenn Goulet', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mary Dye', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Brett Borden', 3,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Joe Schmick', 2,2)

SET @catid = '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01'    -- Legislative District 10
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Ron Muzzall', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Helen Price Johnson', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Greg Gilday', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Angie Homola', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bill Bruch', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Dave Paul', 1,2)

SET @catid = 'D6CB6AFD-6802-4061-A244-A0F8F2A97541'    -- Legislative District 11
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bob Hasegawa', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'David Hackney', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jack Hudgins', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Steve Bergquist', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Sean Atchison', 1,2)

SET @catid = '4ED33B65-B9BD-4FEA-9B53-96ADBB2F75E5'    -- Legislative District 12
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Brad Hawkins', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Keith Goehner', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Adrianne Moore', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mike Steele', 2,2)

SET @catid = '60E04F71-C117-452C-A20B-A74E44A95F40'    -- Legislative District 13
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tom Dent', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Eduardo Castaneda-Diaz', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Alex Ybarra', 2,2)

SET @catid = 'F509843F-1E86-4BEF-AD2B-246DD212BC7B'    -- Legislative District 14
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Curtis P. King', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Chris Corry', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tracy Rushing', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Gina Mosbrucker', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Devin Kuh', 1,2)

SET @catid = '70D8BD8D-60F5-44D7-895B-17941C0C37C9'    -- Legislative District 15
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bruce Chandler', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jack McEntire', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jeremie Dufault', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'AJ Cooper', 1,2)

SET @catid = '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717'    -- Legislative District 16
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Danielle Garbe Reser', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Perry Dozier', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mark Klicker', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Frances Chvatal', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Skyler Rude', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Carly Coburn', 1,2)

SET @catid = '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740'    -- Legislative District 17
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Lynda Wilson', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Daniel Smith', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Vicki Kraft', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tanish L. Harris', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Paul Harris', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bryn White', 1,2)

SET @catid = '41BD9562-CC7F-47FF-B3A0-D6CC682804D5'    -- Legislative District 18
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Rick Bell', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Ann Rivers', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Brandon Vick', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kassandra Bessert', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Larry Hoeff', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Donna L. Sinclair', 1,2)

SET @catid = 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830'    -- Legislative District 19
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Dean Takko', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jeff Wilson', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jim Walsh', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Marianna Everson', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Joel McEntire', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Brian E. Blake', 1,2)

SET @catid = 'ABC7474C-398E-4E7C-8C90-24A3BEABA05B'    -- Legislative District 20
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'John Braun', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Peter Abbarno', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Timothy Zahn', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Ed Orcutt', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Will Rollet', 1,2)

SET @catid = '1440BA1C-23CE-4295-A14D-C2C03CE80267'    -- Legislative District 21
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Strom H. Peterson', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Brian Thompson', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Lillian Ortiz-Self', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Amy Schaper', 2,2)

SET @catid = 'B53388ED-FCAA-4281-834E-02D5236A4457'    -- Legislative District 22
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Sam Hunt', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Garry Holland', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Laurie Dolan', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'J.D. Ingram', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Dusty Pierpoint', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jessica Bateman', 1,2)

SET @catid = 'EDBA1A3F-0427-4970-9961-7B486DDF822C'    -- Legislative District 23
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Christine Rolfes', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Pam Madden-Boyer', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tarra Simmons', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'April Ferguson', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Drew Hansen', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Elaina Bonzales-Blanton', 2,2)

SET @catid = '1780068E-8145-4AB3-9786-E1956CF62E8A'    -- Legislative District 24
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kevin Van De Wege', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Connie Beauvais', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mike Chapman', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Sue Forde', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Steve Tharinger', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Brian Pruiett', 2,2)

SET @catid = '8466A090-EB67-423C-B619-C9F0D1BF8A11'    -- Legislative District 25
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Chris Glidon', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Julie Door', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kelly Chambers', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jamie Smith', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Cyndy Jacobsen', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Brian Duthie', 1,2)

SET @catid = '1DDBE15B-C1FF-4446-87F2-2649FA9FB3E3'    -- Legislative District 26
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jesse L. Young', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Carrie Hesch', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Joy Standford', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Michelle Caldier', 2,2)

SET @catid = 'C07F9B4B-8358-46AC-B2F6-7893084E6F23'    -- Legislative District 27
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jeannie Darneille', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kyle Paskewitz', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Laurie Jinkins', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Ryan Talen', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jake Fey', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Barry Knowles', 2,2)

SET @catid = '8FF3A9B3-998F-4377-BAF1-3D90156C93F0'    -- Legislative District 28
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Twina Nobles', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Steve OBan', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mari Leavitt', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kevin Ballard', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Dan Bronske', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Chris Nye', 2,2)

SET @catid = '7A5C5348-DAB6-4DC7-8812-F000C7FAD2AB'    -- Legislative District 29
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Melanie Morgan', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Koshin Mohamed Fidaar', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Terry Harder', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Steve Kirby', 1,2)

SET @catid = '07C28152-B519-4545-A5A1-24DA1EF5C571'    -- Legislative District 30
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jamila Taylor', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Martin A. Moore', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jesse Johnson', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jack Walsh', 2,2)

SET @catid = 'DB2E6C95-E19F-453D-9347-FC15281D16F3'    -- Legislative District 31
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Drew Stokesbary', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Katie Young', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Eric E. Robertson', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Thomas R. Clark', 1,2)


SET @catid = '97FE1731-9E87-47CE-B054-FB6A350E8799'    -- Legislative District 32
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Cindy Ryu', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Shirley Sutton', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Lauren Davis', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tamra Smilanich', 2,2)

SET @catid = '93557631-4902-4E2F-A523-083801F454B8'    -- Legislative District 33
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tina L. Orwall', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kerry French', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mia Su-Ling Gregerson', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Marliza Melzer', 3,2)

SET @catid = '3BC26911-F7CB-4086-9770-4AA8E6E18DF7'    -- Legislative District 34
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Eileen L. Cody', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jow Fitzgibbon', 1,2)

SET @catid = 'FB09D766-4A37-4415-8C5E-DB5796D448EE'    -- Legislative District 35
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Dan Griffey', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Colton Myers', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Drew C. MacEwen', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Darcey Huffman', 1,2)

SET @catid = 'F825F254-F04B-4410-9D67-6BB42A1BC612'    -- Legislative District 36
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Noel Christina Frame', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Liz Berry', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Sarah Reyneveld', 1,2)

SET @catid = 'FB880F79-88BA-4823-A7D4-A73980F81B1A'    -- Legislative District 37
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Sharon Tomiko Santos', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'John Stafford', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kirsten Harris-Talley', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Chukundi Salisbury', 1,2)

SET @catid = 'CCFC5848-1F3A-4EE8-830D-623C1183D60A'    -- Legislative District 38
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'June Robinson', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bernard Moody', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Emily Wicks', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Bert Johnson', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mike Sells', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'David Wiley', null,2)

SET @catid = '666E494C-D7C7-49C2-8288-5FB16049EBC4'    -- Legislative District 39
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Keith L. Wagoner', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kathryn A. Lewandowsky', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Robert J. Sutherland', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Claus Joens', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Carolyn Eslick', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Ryan Johnson', 1,2)

SET @catid = '5DCD5262-3A30-4DDA-A8C3-AAD5DF3444D9'    -- Legislative District 40
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Elizabeth Lovelett', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Charles Carrell', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Debra Lekanoff', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Alex Ramel', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Russ Dzialo', 2,2)

SET @catid = '4359DC0A-011E-48D3-9A1C-0AF54812457C'    -- Legislative District 41
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Lisa Wellman', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mike Nykreim', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tana Senn', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'My-Linh Thai', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Al Rosenthal', 2,2)

SET @catid = '57592DEB-EDF9-4F37-AEDD-89F41968D464'    -- Legislative District 42
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Luanne Van Werven', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Alice Rule', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Jennifer Sefzik', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Sharon Shewmake', 1,2)

SET @catid = '6719E611-104B-4605-B16D-96DE62D9E5E1'    -- Legislative District 43
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Nicole Macri', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Leslie Klein', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Frank Chopp', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Sherae Lascelles', 2,2)

SET @catid = '0B56CDFA-4930-4427-A132-887F3C789C71'    -- Legislative District 44
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'John Lovick', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'John T. Kartak', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Mark A. James', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'April Berg', 1,2)

SET @catid = 'D6FF789D-8476-4079-85C1-1324F6FDA740'    -- Legislative District 45
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Roger Goodman', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'John P. Gibbons', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Larry Springer', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Amber Krabach', 2,2)

SET @catid = '1986C94B-3384-4716-B9ED-E6752D8230A5'    -- Legislative District 46
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Gerry Pollet', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Eric J. Brown', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Javier Valdez', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Beth Daranciang', 2,2)

SET @catid = '8E28DC52-ED49-4CDC-9FE2-77BA4E754E48'    -- Legislative District 47
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Debra Entenman', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Kyle Lyebyedyev', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Pat Sullivan', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Ted Cooke', 2,2)

SET @catid = '9A3DF9D3-A0E4-4E37-84D3-442A18F4CC25'    -- Legislative District 48
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Vandana Slatter', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Victor H. Bishop', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Amy Walen', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Tim J. Hickey', 2,2)

SET @catid = '3085C76F-74BE-419C-9D79-9500955B6CEF'    -- Legislative District 49
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Annette Cleveland', 1,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Rey Reynolds', 2,1)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Sharon Wylie', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Justin Forsman', 2,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Monica Stonier', 1,2)
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (NEWID(), @electionid, @catid, 'Park Llafet', 2,2)

-- judicial
SET @catid = 'D1385E48-9803-4753-B925-3AE36376113E'    -- cat76
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Dave Larson')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Raquel Montoya-Lewis')
SET @catid = '2DF69364-F2CA-4733-9B8D-9A81A57F6698'    -- cat77
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Charles W. Johnson')
SET @catid = 'E50D3BD4-87FB-49A4-8605-1C0123846905'    -- cat78
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Richard S. Serns')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'G. Helen Whitener')
SET @catid = '4615A8AE-678F-472A-AB8D-116F776AD47E'    -- cat79
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Debra L. Stephens')
SET @catid = '9DC2D1CC-0C3D-45F6-AC21-3F1F9B564CAD'    -- cat80  Court of Appeals
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'David S. Mann')
SET @catid = '52780E2E-DC1C-4610-BBBE-DAAD451C14CC'    -- cat81
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Bill Bowman')
SET @catid = 'DBEE79DF-090A-4BBD-BDC1-CA1E26EF93C4'    -- cat82
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Linda Coburn')
SET @catid = '57A02606-B364-435C-B235-D6C8CFC2D5A8'    -- cat83
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Lisa Worswick')
SET @catid = '6FAEB3E1-96A9-4087-A193-87CA47585528'    -- cat84
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Lisa L. Sutton')
SET @catid = '6D3BF1BC-E842-43B6-B354-AA9BB76A00B8'    -- cat85
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Marshall Casey')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Tracy Arlene Staab')
SET @catid = 'CF6EDB6E-D27F-4E30-9406-36EE3DAB9000'    -- cat86
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Rebecca Pennell')
SET @catid = 'A8B4E3A6-ED63-4532-A0A7-AAAF45F6D202'    -- cat87  Adams Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Steve Dixon')
SET @catid = '13E22951-5111-4DC0-BEAE-D90893C4266B'    -- cat88  Asotin, Columbia
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Brook J. Burns')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'G. Scott Marinella')
SET @catid = '3DCDCB4A-F5D5-43FC-90A8-3FB40C17E82A'    -- cat89  Benton, Franklin Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Dave Peterson')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Sharon Brown')
SET @catid = '5FC9EC2E-7EEE-44FB-9D75-39FB523EAE04'    -- cat90
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Joe Burrowes')
SET @catid = '6875B187-7301-463D-95A0-B11A947E27D7'    -- cat91
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Alexander Carl Ekstrom')
SET @catid = 'B257F910-E196-4C98-93F5-1CD6A97F15B7'    -- cat92
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Cameron Mitchell')
SET @catid = '71FDE414-3214-4E08-B997-F60D8F84306B'    -- cat93
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Sam Swanberg')
SET @catid = 'C37795EE-AFC1-4A9C-A6A9-89BEDAB513AD'    -- cat94
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Carrie Runge')
SET @catid = '8FBBF357-76BB-40BE-8DFB-194CA6FFF0D8'    -- cat95
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Jacqueline Shea Brown')
SET @catid = 'AC9372F8-86C1-43A1-8094-D0DB5479FC80'    -- cat96  Chelan
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Lesley A. Allan')
SET @catid = '917D2575-5F0E-4F1B-9E9F-F9318482C0DC'    -- cat97
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Travis Brandt')
SET @catid = 'D90683BD-E239-4556-A592-EDC6A1DC8242'    -- cat98
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Kristin Ferra')
SET @catid = '75F11078-8071-4685-98F9-9AAA200C0FDE'    -- cat99  Clallam
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Lauren Erickson')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Lisa N. Dublin')
SET @catid = '85B4CE0B-12C0-4998-97CF-5A70C7552D3F'    -- cat100
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Simon Barnhart')
SET @catid = 'F043F886-D2BC-447B-9B99-9DB7147E1E00'    -- cat101
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Brent Basden')
SET @catid = '303BE419-F3C7-4DB8-9396-4DF15322365F'    -- cat102  Douglas
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Brian C. Huber')
SET @catid = '2DDFCC8A-D54E-4B43-BB40-9C392260F876'    -- cat103  Ferry, Pend Oriville
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Patrick A. Monasmith')
SET @catid = '3EDC2EDA-18BE-41C1-B062-CC816E63280A'    -- cat104
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Jessica Taylor Reeves')
SET @catid = 'C4738955-D00A-4713-AD9E-EF604F20FEFE'    -- cat105  Grant Superior court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'David Estudillo')
SET @catid = '325273D6-90F7-4F84-8ECD-FB163523E03E'    -- cat106
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'John D. Knodell')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Kevin J. McCrae')
SET @catid = 'CBFFBE19-5C16-4633-AF0C-076843A751D0'    -- cat107
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'John Antosz')
SET @catid = 'FECFCCED-7ECF-4343-A3D2-157493B6842A'    -- cat108  Grays Harbor
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Katie Svoboda')
SET @catid = 'D7DC0FA1-6420-4C53-80CA-3A6781C4C79E'    -- cat109
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'David L. Edwards')
SET @catid = '64659BDD-8FDE-4C40-A6C3-7AB639D96336'    -- cat110
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'David Mistachkin')
SET @catid = '88300B76-31DF-4B4E-AB1D-E892A88DC463'    -- cat111  Island
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Christon C. Skinner')
SET @catid = '10F8BCF9-7763-4DC3-B00A-4FE860643AA1'    -- cat112
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Kathleen Petrich')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Carolyn Cliff')
SET @catid = '36C039A8-EBDD-4DAA-8C07-E621C35A1500'    -- cat113  Jefferson
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Keith C. Harper')
SET @catid = '400D5F5C-6847-4274-991F-2A5F2F350714'    -- cat114  King Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Hillary Madsen')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Andrea Robertson')
SET @catid = '3F385BB9-99CB-4E08-BE9E-9C543206AED2'    -- cat115
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Doug North')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Carolyn Ladd')
SET @catid = '51E61D09-155E-4899-99E3-58D1688D9E41'    -- cat116  Kitsap
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Tina Robinson')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Lynn Fleischbein')
SET @catid = '9C573249-EBBB-4E14-84E4-EBD7A384FA13'    -- cat117 Kittitas
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Candace Hooper')
SET @catid = '6CBF1121-1433-4D1E-9E60-E01E654A76DE'    -- cat118
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Scott R. Sparks')
SET @catid = 'F311D6F3-FB68-4242-8942-9F8CCF9FB124'    -- cat119  Klickitat
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Randall Krog')
SET @catid = 'C4D9613C-04F6-4E8A-8F56-DABE9B67A867'    -- cat120  Lewis Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'J. Andrew Toynbee')
SET @catid = '9900BAF5-497D-433C-9BC0-99A085315C98'    -- cat121
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'James W. Lawler')
SET @catid = '2FD9FD9D-79A7-4004-B1A7-74912D5D044F'    -- cat122
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Joely ORourke')
SET @catid = 'E03341CA-7059-440B-913D-7CF0F423F79D'    -- cat123  Lincoln
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Jeff Barkdull')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Dan B. Johnson')
SET @catid = '21DF405C-75B9-49B1-9733-7A974AC99945'    -- cat124  Mason Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Amber Finlay')
SET @catid = 'DD9251F6-6D90-4E00-9AD2-A717A60F0F8E'    -- cat125
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Monty D. Cobb')
SET @catid = '1D0E3106-D611-4FE9-8F42-B7E725F8383F'    -- cat126
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Daniel Goodell')
SET @catid = '4CE99A7D-A009-4AED-A6A4-F39E38018CE3'    -- cat127  Okanogan
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Henry Hank Rawson')
SET @catid = '096DA2A0-6C07-4611-AC8C-3A0B0A9CE07C'    -- cat128  
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Chris Culp')
SET @catid = 'CA02E3CD-7F2A-464A-8C72-AB3D000A0635'    -- cat129  Pacific
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Donald J. Richter')
SET @catid = '16B8C5FD-99D4-47CC-8487-7E2EE792BDC9'    -- cat130  Pierce
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Brad J. Horenstein')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Bryan Chushcoff')
SET @catid = '10D26CAA-3442-4EF8-9415-3D4E8D170681'    -- cat131  San Juan
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Kathryn C. Loring')
SET @catid = '436F0897-B812-45E5-A09F-E9F70F09A591'    -- cat132  Skagit
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Tom Seguine')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Elizabeth Yost Neidzwski')
SET @catid = '51843242-AAD2-4EA1-A540-C54D3DC3B2CA'    -- cat133 Snohomish
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Robert Grant')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Cassandra Lopez Shaw')
SET @catid = 'E8EFCB4B-871D-49FF-A0BD-D003004E7C99'    -- cat134  Thurston Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Sharonda D. Amamilo')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Scott Ahlf')
SET @catid = 'EC4B58DD-AACE-4ACC-A2F6-C846DA3E1A33'    -- cat135  Walla Walla Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Michael S. Mitchell')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Brandon L. Johnson')
SET @catid = '5E12BF1F-537B-4D9D-8947-868B7D6C6A83'    -- cat136
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'M. Scott Wolfram')
SET @catid = 'A9AEF769-6B11-4B7B-8ECC-5A799148CFA9'    -- cat137 Whatcom Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Evan Jones')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'James Erb')
SET @catid = '0F1EEFCF-4508-439E-9D0F-ADE8838C588B'    -- cat138
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'David E. Freeman')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Jim Nelson')
SET @catid = 'CFA8040B-D227-4E59-9DDC-C85FB0435965'    -- cat139 Whitman Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Gary J. Libey')
SET @catid = 'C8326398-88E8-4ECA-BA02-E8DF9CA6EFBC'    -- cat140  Yakima Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Elsabeth Tutsch')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Bronson Faul')
SET @catid = '17857B11-846A-4CF0-82A0-29102992A39C'    -- cat141
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Doublas L. Federspiel')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Jeff Swan')
SET @catid = '11C55A61-0C7D-4778-AA66-4110F95D79CF'    -- cat142
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Scott Bruns')
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (NEWID(), @electionid, @catid, 'Blaine Grorge Gibson')
GO

ALTER table Ticket CHECK CONSTRAINT ALL
ALTER table Category CHECK CONSTRAINT ALL


GO

