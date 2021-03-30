create extension if not exists "uuid-ossp";
--declare @electionId uuid
----SET @electionId = 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6'

--declare @catid uuid
-- measures
--SET @catid = '658F3CDB-FBE2-44EC-B258-6811FA63271C'    --cat1
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '658F3CDB-FBE2-44EC-B258-6811FA63271C', 'Approved', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '658F3CDB-FBE2-44EC-B258-6811FA63271C', 'Rejected', 2);
--SET @catid = '3D67CFD2-2645-44EB-8196-02E729875D0B'    -- cat2
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3D67CFD2-2645-44EB-8196-02E729875D0B', 'Repealed', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3D67CFD2-2645-44EB-8196-02E729875D0B', 'Maintained', 2);
--SET @catid = '6AF40813-D42C-4DA0-A8E0-FE61D922C680'   --cat3
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6AF40813-D42C-4DA0-A8E0-FE61D922C680' , 'Repealed', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6AF40813-D42C-4DA0-A8E0-FE61D922C680' , 'Maintained', 2);
--SET @catid = '716C38A8-6CA2-427E-9FEC-763B997F3C54'   --cat4
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '716C38A8-6CA2-427E-9FEC-763B997F3C54' , 'Repealed', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '716C38A8-6CA2-427E-9FEC-763B997F3C54' , 'Maintained', 2);
--SET @catid = '9650C476-C579-4D4D-887D-9C154A95239B'   --cat5
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9650C476-C579-4D4D-887D-9C154A95239B', 'Repealed', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','9650C476-C579-4D4D-887D-9C154A95239B', 'Maintained', 2);
--SET @catid = '83F84069-0DD1-4E13-833B-737E85F64CFF'  --cat6
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6',  '83F84069-0DD1-4E13-833B-737E85F64CFF', 'Approved', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6',  '83F84069-0DD1-4E13-833B-737E85F64CFF', 'Rejected', 2);


-- federal
--SET @catid = '90545980-C4BC-48D7-BE0A-97730AAD3972'   --cat7
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '90545980-C4BC-48D7-BE0A-97730AAD3972' , 'Joseph R. Biden / Kamala D. Harris', 1, 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '90545980-C4BC-48D7-BE0A-97730AAD3972' , 'Donald J. Trump / Michael R. Pence', 2, 2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '90545980-C4BC-48D7-BE0A-97730AAD3972' , 'Jo Jorgensen / Jeremy "Spike" Cohen', 3, 3);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '90545980-C4BC-48D7-BE0A-97730AAD3972' , 'Howie Hawkins / Angela Walker', 4, 4);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '90545980-C4BC-48D7-BE0A-97730AAD3972' , 'Gloris La Riva / Sunil Freeman', 5, 5);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, Sequence) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '90545980-C4BC-48D7-BE0A-97730AAD3972' , 'Alyson Kennedy / Malcolm M. Jarrett', 6, 6);
-- federal congressional disticts
--SET @catid = 'D8B38531-E9C0-4B29-A368-4791DA8090F3'   --cat8
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D8B38531-E9C0-4B29-A368-4791DA8090F3', 'Suzan DelBene', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D8B38531-E9C0-4B29-A368-4791DA8090F3', 'Jeffrey Beeler, Sr', 2);
--SET @catid = '667BD869-D334-48E7-8C20-827B5F6983BC'  --cat9
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '667BD869-D334-48E7-8C20-827B5F6983BC', 'Rick Larsen', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '667BD869-D334-48E7-8C20-827B5F6983BC', 'Timothy S. Hazelo', 2);
--SET @catid = '70EFE57B-FCB0-46C0-8559-230B8D6C301E'  --cat10
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '70EFE57B-FCB0-46C0-8559-230B8D6C301E', 'Jaime Herrera Beutler', 2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '70EFE57B-FCB0-46C0-8559-230B8D6C301E', 'Carolyn Long', 1);
--SET @catid = '0BFF8D66-AA56-4A99-9FFE-DFABF4A0A6C9'  --cat11
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '0BFF8D66-AA56-4A99-9FFE-DFABF4A0A6C9', 'Dan Newhouse', 2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '0BFF8D66-AA56-4A99-9FFE-DFABF4A0A6C9', 'Douglas E. McKinley', 1);
--SET @catid = '8DBCD15C-B7BF-4E25-B5FA-EF2B9300FA0D'  --cat12
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8DBCD15C-B7BF-4E25-B5FA-EF2B9300FA0D', 'Cathy McMorris Rodgers', 2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8DBCD15C-B7BF-4E25-B5FA-EF2B9300FA0D', 'Dave Wilson', 1);
--SET @catid = '29673FC3-CFC9-47D8-87F6-6389E6089FB3'  --cat13
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '29673FC3-CFC9-47D8-87F6-6389E6089FB3', 'Derek Kilmer', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '29673FC3-CFC9-47D8-87F6-6389E6089FB3', 'Elizabeth Kreiselmaier', 2);
--SET @catid = '95B46107-D6BB-4EDF-8D5D-87C648FCE7DB'  --cat14
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','95B46107-D6BB-4EDF-8D5D-87C648FCE7DB' , 'Pramila Jayapal', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '95B46107-D6BB-4EDF-8D5D-87C648FCE7DB' , 'Craig Keller', 2);
--SET @catid = '05B6D9DC-73EE-4E1C-A352-AB6FBF91D208'  --cat15
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','05B6D9DC-73EE-4E1C-A352-AB6FBF91D208' , 'Kim Schrier', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '05B6D9DC-73EE-4E1C-A352-AB6FBF91D208' , 'Jesse Jensen', 2);
--SET @catid = 'BD29F494-E223-47CE-8385-ED98CD096ABE'  --cat16
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'BD29F494-E223-47CE-8385-ED98CD096ABE', 'Adam Smith', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'BD29F494-E223-47CE-8385-ED98CD096ABE', 'Doug Basler', 2);
--SET @catid = 'DE7F37F2-C9DB-45B6-A5FA-DFFE67EB394A'  --cat17
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'DE7F37F2-C9DB-45B6-A5FA-DFFE67EB394A' , 'Marilyn Strickland', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'DE7F37F2-C9DB-45B6-A5FA-DFFE67EB394A' , 'Beth Doglio', 2);

-- statewide
--SET @catid = '383F37D2-4613-4CF2-A83C-7E004251CA2F'  --cat18
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '383F37D2-4613-4CF2-A83C-7E004251CA2F', 'Jay Inslee', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '383F37D2-4613-4CF2-A83C-7E004251CA2F', 'Loren Culp', 2);
--SET @catid = '5AD15A1E-593E-45FE-B149-BE43B91FC82B'  --cat19
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','5AD15A1E-593E-45FE-B149-BE43B91FC82B', 'Denny Heck', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '5AD15A1E-593E-45FE-B149-BE43B91FC82B', 'Marko Liias', 1);
--SET @catid = '6383E314-2111-45DE-A0E4-908AB83E97D5'  --cat20
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6383E314-2111-45DE-A0E4-908AB83E97D5', 'Kim Wyman', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6383E314-2111-45DE-A0E4-908AB83E97D5', 'Gael Tarleton', 2);
--SET @catid = '9B4E9F03-E39F-4BB5-8838-F6137EF391C1'  --cat21
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9B4E9F03-E39F-4BB5-8838-F6137EF391C1', 'Mike Pellicciotti', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9B4E9F03-E39F-4BB5-8838-F6137EF391C1', 'Duane A. Davidson', 2);
--SET @catid = 'C33EC949-6CE7-4F5B-A863-B3EE792247EC'  --cat22
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C33EC949-6CE7-4F5B-A863-B3EE792247EC', 'Pat McCarthy', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C33EC949-6CE7-4F5B-A863-B3EE792247EC', 'Chris Leyba', 2);
--SET @catid = '3C54BD34-AB8A-4F13-810F-F128DAA8766E'  --cat23
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3C54BD34-AB8A-4F13-810F-F128DAA8766E', 'Bob Ferguson', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3C54BD34-AB8A-4F13-810F-F128DAA8766E', 'Matt Larkin', 2);
--SET @catid = '4E796214-2FD1-4177-9F1D-24BA13BAAE43'  --cat24
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4E796214-2FD1-4177-9F1D-24BA13BAAE43', 'Hilary Franz', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4E796214-2FD1-4177-9F1D-24BA13BAAE43', 'Sue Kuehl Pederson', 2);
--SET @catid = '3FE4CFD6-526C-4D6A-AF56-E12EAE745ABC'  --cat25
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3FE4CFD6-526C-4D6A-AF56-E12EAE745ABC', 'Chris Reykdal', null);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3FE4CFD6-526C-4D6A-AF56-E12EAE745ABC', 'Maia Espinoza', null);
--SET @catid = 'A983A6BB-98C0-44B6-A3CD-4E8E38BBF165'  --cat26
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A983A6BB-98C0-44B6-A3CD-4E8E38BBF165', 'Mike Kreidler', 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A983A6BB-98C0-44B6-A3CD-4E8E38BBF165', 'Chirayu Avinash Patel', 2);

--Legislative
--SET @catid = 'E5CAB96F-D5C8-4835-9313-FA503E3C44CC'    -- Legislative District 1
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E5CAB96F-D5C8-4835-9313-FA503E3C44CC', 'Derek Stanford', 1, 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E5CAB96F-D5C8-4835-9313-FA503E3C44CC', 'Art Coday', 2, 1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E5CAB96F-D5C8-4835-9313-FA503E3C44CC', 'Davina Duerr', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','E5CAB96F-D5C8-4835-9313-FA503E3C44CC', 'Adam Bartholomew', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E5CAB96F-D5C8-4835-9313-FA503E3C44CC', 'Shelly Kloba', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E5CAB96F-D5C8-4835-9313-FA503E3C44CC', 'Jeb Brewer', 2,2);
--SET @catid = '75AC1826-6092-4E79-B3A6-662AEB557FCB'    -- Legislative District 2
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '75AC1826-6092-4E79-B3A6-662AEB557FCB', 'Rick Payne', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '75AC1826-6092-4E79-B3A6-662AEB557FCB', 'Jim McCune', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '75AC1826-6092-4E79-B3A6-662AEB557FCB', 'Andrew Barkis', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '75AC1826-6092-4E79-B3A6-662AEB557FCB', 'JT Wilcox', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '75AC1826-6092-4E79-B3A6-662AEB557FCB', 'Veronica Whitcher Rockett', 1,2);
--SET @catid = 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3'    -- Legislative District 3
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3' , 'Andy Billig', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3' , 'Dave Lucas', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3' , 'Marcus Riccelli', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3' , 'Laura D Carder', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3' , 'Timm Ormsby', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3' , 'Bob Apple', 2,2);

--SET @catid = '77DA07AA-A491-4426-B4DA-9B9843C754A1'    -- Legislative District 4
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '77DA07AA-A491-4426-B4DA-9B9843C754A1' , 'Mike Padden', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '77DA07AA-A491-4426-B4DA-9B9843C754A1' , 'John Roskelley', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '77DA07AA-A491-4426-B4DA-9B9843C754A1' , 'Bob McCaslin', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '77DA07AA-A491-4426-B4DA-9B9843C754A1' , 'Lori Feagan', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '77DA07AA-A491-4426-B4DA-9B9843C754A1' , 'Lance Gurel', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '77DA07AA-A491-4426-B4DA-9B9843C754A1' , 'Rob Chase', 2,2);

--SET @catid = '538F1962-C23B-4004-8561-CEAD17639EAA'    -- Legislative District 5
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '538F1962-C23B-4004-8561-CEAD17639EAA', 'Ingrid Anderson', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '538F1962-C23B-4004-8561-CEAD17639EAA', 'Mark Mullet', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '538F1962-C23B-4004-8561-CEAD17639EAA', 'Bill Ramos', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '538F1962-C23B-4004-8561-CEAD17639EAA', 'Ken Moninski', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '538F1962-C23B-4004-8561-CEAD17639EAA', 'Lisa Callan', 1,2);

--SET @catid = '29313286-0E18-4D70-ACB6-549EF8421B5B'   -- Legislative District 6
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '29313286-0E18-4D70-ACB6-549EF8421B5B', 'Mike Volz', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','29313286-0E18-4D70-ACB6-549EF8421B5B', 'Zack Zappone', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '29313286-0E18-4D70-ACB6-549EF8421B5B', 'Jenny Graham', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '29313286-0E18-4D70-ACB6-549EF8421B5B', 'Tom McGarry', 1,2);

--SET @catid = '7B5026A3-D8AA-4EB2-84DD-E7B4A58199E3'   -- Legislative District 7
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '7B5026A3-D8AA-4EB2-84DD-E7B4A58199E3', 'Jacquelin Maycumber', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '7B5026A3-D8AA-4EB2-84DD-E7B4A58199E3', 'Georgia D. Davenport', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '7B5026A3-D8AA-4EB2-84DD-E7B4A58199E3', 'Joel Kretz', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '7B5026A3-D8AA-4EB2-84DD-E7B4A58199E3', 'JJ Wandler', 3,2);

--SET @catid = 'A31CC2C5-5AEE-497C-83B4-ADF7AC27306C'   -- Legislative District 8
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A31CC2C5-5AEE-497C-83B4-ADF7AC27306C', 'Brad Klippert', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A31CC2C5-5AEE-497C-83B4-ADF7AC27306C', 'Shir Regev', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A31CC2C5-5AEE-497C-83B4-ADF7AC27306C', 'Matt Boehnke', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A31CC2C5-5AEE-497C-83B4-ADF7AC27306C', 'Larry Stanley', 2,2);

--SET @catid = 'BABE5E86-5342-4A59-A2FD-F13CE8DB3FAA'    -- Legislative District 9 cat35
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'BABE5E86-5342-4A59-A2FD-F13CE8DB3FAA' , 'Mark G. Schoesler', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'BABE5E86-5342-4A59-A2FD-F13CE8DB3FAA' , 'Jenn Goulet', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'BABE5E86-5342-4A59-A2FD-F13CE8DB3FAA' , 'Mary Dye', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'BABE5E86-5342-4A59-A2FD-F13CE8DB3FAA' , 'Brett Borden', 3,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'BABE5E86-5342-4A59-A2FD-F13CE8DB3FAA' , 'Joe Schmick', 2,2);

--SET @catid = '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01'    -- Legislative District 10
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01', 'Ron Muzzall', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01', 'Helen Price Johnson', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01', 'Greg Gilday', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01', 'Angie Homola', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01', 'Bill Bruch', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01', 'Dave Paul', 1,2);

--SET @catid = 'D6CB6AFD-6802-4061-A244-A0F8F2A97541'    -- Legislative District 11
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D6CB6AFD-6802-4061-A244-A0F8F2A97541', 'Bob Hasegawa', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D6CB6AFD-6802-4061-A244-A0F8F2A97541', 'David Hackney', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D6CB6AFD-6802-4061-A244-A0F8F2A97541', 'Jack Hudgins', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','D6CB6AFD-6802-4061-A244-A0F8F2A97541', 'Steve Bergquist', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D6CB6AFD-6802-4061-A244-A0F8F2A97541', 'Sean Atchison', 1,2);

--SET @catid = '4ED33B65-B9BD-4FEA-9B53-96ADBB2F75E5'    -- Legislative District 12
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4ED33B65-B9BD-4FEA-9B53-96ADBB2F75E5', 'Brad Hawkins', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4ED33B65-B9BD-4FEA-9B53-96ADBB2F75E5', 'Keith Goehner', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4ED33B65-B9BD-4FEA-9B53-96ADBB2F75E5', 'Adrianne Moore', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4ED33B65-B9BD-4FEA-9B53-96ADBB2F75E5', 'Mike Steele', 2,2);

--SET @catid = '60E04F71-C117-452C-A20B-A74E44A95F40'    -- Legislative District 13
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '60E04F71-C117-452C-A20B-A74E44A95F40' , 'Tom Dent', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '60E04F71-C117-452C-A20B-A74E44A95F40' , 'Eduardo Castaneda-Diaz', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '60E04F71-C117-452C-A20B-A74E44A95F40' , 'Alex Ybarra', 2,2);

--SET @catid = 'F509843F-1E86-4BEF-AD2B-246DD212BC7B'    -- Legislative District 14
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F509843F-1E86-4BEF-AD2B-246DD212BC7B', 'Curtis P. King', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F509843F-1E86-4BEF-AD2B-246DD212BC7B', 'Chris Corry', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F509843F-1E86-4BEF-AD2B-246DD212BC7B', 'Tracy Rushing', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F509843F-1E86-4BEF-AD2B-246DD212BC7B', 'Gina Mosbrucker', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F509843F-1E86-4BEF-AD2B-246DD212BC7B', 'Devin Kuh', 1,2);

--SET @catid = '70D8BD8D-60F5-44D7-895B-17941C0C37C9'    -- Legislative District 15
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '70D8BD8D-60F5-44D7-895B-17941C0C37C9', 'Bruce Chandler', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','70D8BD8D-60F5-44D7-895B-17941C0C37C9', 'Jack McEntire', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '70D8BD8D-60F5-44D7-895B-17941C0C37C9', 'Jeremie Dufault', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '70D8BD8D-60F5-44D7-895B-17941C0C37C9', 'AJ Cooper', 1,2);

--SET @catid = '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717'    -- Legislative District 16
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717', 'Danielle Garbe Reser', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717', 'Perry Dozier', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717', 'Mark Klicker', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717' , 'Frances Chvatal', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717' , 'Skyler Rude', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717' , 'Carly Coburn', 1,2);

--SET @catid = '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740'    -- Legislative District 17
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740', 'Lynda Wilson', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740', 'Daniel Smith', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740', 'Vicki Kraft', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740', 'Tanish L. Harris', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740', 'Paul Harris', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740', 'Bryn White', 1,2);

--SET @catid = '41BD9562-CC7F-47FF-B3A0-D6CC682804D5'    -- Legislative District 18
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '41BD9562-CC7F-47FF-B3A0-D6CC682804D5', 'Rick Bell', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '41BD9562-CC7F-47FF-B3A0-D6CC682804D5', 'Ann Rivers', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '41BD9562-CC7F-47FF-B3A0-D6CC682804D5', 'Brandon Vick', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '41BD9562-CC7F-47FF-B3A0-D6CC682804D5', 'Kassandra Bessert', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '41BD9562-CC7F-47FF-B3A0-D6CC682804D5', 'Larry Hoeff', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '41BD9562-CC7F-47FF-B3A0-D6CC682804D5', 'Donna L. Sinclair', 1,2);

--SET @catid = 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830'    -- Legislative District 19
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830', 'Dean Takko', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830', 'Jeff Wilson', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830', 'Jim Walsh', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830', 'Marianna Everson', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830', 'Joel McEntire', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830', 'Brian E. Blake', 1,2);

--SET @catid = 'ABC7474C-398E-4E7C-8C90-24A3BEABA05B'    -- Legislative District 20
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'ABC7474C-398E-4E7C-8C90-24A3BEABA05B', 'John Braun', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'ABC7474C-398E-4E7C-8C90-24A3BEABA05B', 'Peter Abbarno', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'ABC7474C-398E-4E7C-8C90-24A3BEABA05B', 'Timothy Zahn', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'ABC7474C-398E-4E7C-8C90-24A3BEABA05B', 'Ed Orcutt', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'ABC7474C-398E-4E7C-8C90-24A3BEABA05B', 'Will Rollet', 1,2);

--SET @catid = '1440BA1C-23CE-4295-A14D-C2C03CE80267'    -- Legislative District 21
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1440BA1C-23CE-4295-A14D-C2C03CE80267' , 'Strom H. Peterson', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1440BA1C-23CE-4295-A14D-C2C03CE80267' , 'Brian Thompson', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1440BA1C-23CE-4295-A14D-C2C03CE80267' , 'Lillian Ortiz-Self', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1440BA1C-23CE-4295-A14D-C2C03CE80267' , 'Amy Schaper', 2,2);

--SET @catid = 'B53388ED-FCAA-4281-834E-02D5236A4457'    -- Legislative District 22
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'B53388ED-FCAA-4281-834E-02D5236A4457', 'Sam Hunt', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'B53388ED-FCAA-4281-834E-02D5236A4457', 'Garry Holland', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'B53388ED-FCAA-4281-834E-02D5236A4457', 'Laurie Dolan', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'B53388ED-FCAA-4281-834E-02D5236A4457', 'J.D. Ingram', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'B53388ED-FCAA-4281-834E-02D5236A4457', 'Dusty Pierpoint', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'B53388ED-FCAA-4281-834E-02D5236A4457', 'Jessica Bateman', 1,2);

--SET @catid = 'EDBA1A3F-0427-4970-9961-7B486DDF822C'    -- Legislative District 23
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'EDBA1A3F-0427-4970-9961-7B486DDF822C', 'Christine Rolfes', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','EDBA1A3F-0427-4970-9961-7B486DDF822C', 'Pam Madden-Boyer', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'EDBA1A3F-0427-4970-9961-7B486DDF822C', 'Tarra Simmons', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'EDBA1A3F-0427-4970-9961-7B486DDF822C', 'April Ferguson', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'EDBA1A3F-0427-4970-9961-7B486DDF822C', 'Drew Hansen', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'EDBA1A3F-0427-4970-9961-7B486DDF822C', 'Elaina Bonzales-Blanton', 2,2);

--SET @catid = '1780068E-8145-4AB3-9786-E1956CF62E8A'    -- Legislative District 24
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1780068E-8145-4AB3-9786-E1956CF62E8A', 'Kevin Van De Wege', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1780068E-8145-4AB3-9786-E1956CF62E8A', 'Connie Beauvais', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1780068E-8145-4AB3-9786-E1956CF62E8A', 'Mike Chapman', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1780068E-8145-4AB3-9786-E1956CF62E8A', 'Sue Forde', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1780068E-8145-4AB3-9786-E1956CF62E8A', 'Steve Tharinger', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1780068E-8145-4AB3-9786-E1956CF62E8A', 'Brian Pruiett', 2,2);

--SET @catid = '8466A090-EB67-423C-B619-C9F0D1BF8A11'    -- Legislative District 25
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8466A090-EB67-423C-B619-C9F0D1BF8A11', 'Chris Glidon', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8466A090-EB67-423C-B619-C9F0D1BF8A11', 'Julie Door', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8466A090-EB67-423C-B619-C9F0D1BF8A11', 'Kelly Chambers', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8466A090-EB67-423C-B619-C9F0D1BF8A11', 'Jamie Smith', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8466A090-EB67-423C-B619-C9F0D1BF8A11', 'Cyndy Jacobsen', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8466A090-EB67-423C-B619-C9F0D1BF8A11', 'Brian Duthie', 1,2);

--SET @catid = '1DDBE15B-C1FF-4446-87F2-2649FA9FB3E3'    -- Legislative District 26
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1DDBE15B-C1FF-4446-87F2-2649FA9FB3E3', 'Jesse L. Young', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1DDBE15B-C1FF-4446-87F2-2649FA9FB3E3', 'Carrie Hesch', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1DDBE15B-C1FF-4446-87F2-2649FA9FB3E3', 'Joy Standford', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1DDBE15B-C1FF-4446-87F2-2649FA9FB3E3', 'Michelle Caldier', 2,2);

--SET @catid = 'C07F9B4B-8358-46AC-B2F6-7893084E6F23'    -- Legislative District 27
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C07F9B4B-8358-46AC-B2F6-7893084E6F23', 'Jeannie Darneille', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C07F9B4B-8358-46AC-B2F6-7893084E6F23', 'Kyle Paskewitz', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C07F9B4B-8358-46AC-B2F6-7893084E6F23', 'Laurie Jinkins', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C07F9B4B-8358-46AC-B2F6-7893084E6F23', 'Ryan Talen', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C07F9B4B-8358-46AC-B2F6-7893084E6F23', 'Jake Fey', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C07F9B4B-8358-46AC-B2F6-7893084E6F23', 'Barry Knowles', 2,2);

--SET @catid = '8FF3A9B3-998F-4377-BAF1-3D90156C93F0'    -- Legislative District 28
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8FF3A9B3-998F-4377-BAF1-3D90156C93F0', 'Twina Nobles', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8FF3A9B3-998F-4377-BAF1-3D90156C93F0', 'Steve OBan', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8FF3A9B3-998F-4377-BAF1-3D90156C93F0', 'Mari Leavitt', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8FF3A9B3-998F-4377-BAF1-3D90156C93F0', 'Kevin Ballard', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8FF3A9B3-998F-4377-BAF1-3D90156C93F0', 'Dan Bronske', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8FF3A9B3-998F-4377-BAF1-3D90156C93F0', 'Chris Nye', 2,2);

--SET @catid = '7A5C5348-DAB6-4DC7-8812-F000C7FAD2AB'    -- Legislative District 29
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '7A5C5348-DAB6-4DC7-8812-F000C7FAD2AB', 'Melanie Morgan', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '7A5C5348-DAB6-4DC7-8812-F000C7FAD2AB', 'Koshin Mohamed Fidaar', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '7A5C5348-DAB6-4DC7-8812-F000C7FAD2AB', 'Terry Harder', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '7A5C5348-DAB6-4DC7-8812-F000C7FAD2AB', 'Steve Kirby', 1,2);

--SET @catid = '07C28152-B519-4545-A5A1-24DA1EF5C571'    -- Legislative District 30
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '07C28152-B519-4545-A5A1-24DA1EF5C571', 'Jamila Taylor', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '07C28152-B519-4545-A5A1-24DA1EF5C571', 'Martin A. Moore', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '07C28152-B519-4545-A5A1-24DA1EF5C571', 'Jesse Johnson', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '07C28152-B519-4545-A5A1-24DA1EF5C571', 'Jack Walsh', 2,2);

--SET @catid = 'DB2E6C95-E19F-453D-9347-FC15281D16F3'    -- Legislative District 31
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'DB2E6C95-E19F-453D-9347-FC15281D16F3', 'Drew Stokesbary', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'DB2E6C95-E19F-453D-9347-FC15281D16F3', 'Katie Young', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'DB2E6C95-E19F-453D-9347-FC15281D16F3', 'Eric E. Robertson', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'DB2E6C95-E19F-453D-9347-FC15281D16F3', 'Thomas R. Clark', 1,2);


--SET @catid = '97FE1731-9E87-47CE-B054-FB6A350E8799'    -- Legislative District 32
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '97FE1731-9E87-47CE-B054-FB6A350E8799', 'Cindy Ryu', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '97FE1731-9E87-47CE-B054-FB6A350E8799', 'Shirley Sutton', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '97FE1731-9E87-47CE-B054-FB6A350E8799', 'Lauren Davis', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '97FE1731-9E87-47CE-B054-FB6A350E8799', 'Tamra Smilanich', 2,2);

--SET @catid = '93557631-4902-4E2F-A523-083801F454B8'    -- Legislative District 33
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '93557631-4902-4E2F-A523-083801F454B8', 'Tina L. Orwall', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '93557631-4902-4E2F-A523-083801F454B8', 'Kerry French', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '93557631-4902-4E2F-A523-083801F454B8', 'Mia Su-Ling Gregerson', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '93557631-4902-4E2F-A523-083801F454B8', 'Marliza Melzer', 3,2);

--SET @catid = '3BC26911-F7CB-4086-9770-4AA8E6E18DF7'    -- Legislative District 34
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3BC26911-F7CB-4086-9770-4AA8E6E18DF7'  , 'Eileen L. Cody', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3BC26911-F7CB-4086-9770-4AA8E6E18DF7'  , 'Jow Fitzgibbon', 1,2);

--SET @catid = 'FB09D766-4A37-4415-8C5E-DB5796D448EE'    -- Legislative District 35
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FB09D766-4A37-4415-8C5E-DB5796D448EE', 'Dan Griffey', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FB09D766-4A37-4415-8C5E-DB5796D448EE', 'Colton Myers', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FB09D766-4A37-4415-8C5E-DB5796D448EE', 'Drew C. MacEwen', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FB09D766-4A37-4415-8C5E-DB5796D448EE', 'Darcey Huffman', 1,2);

--SET @catid = 'F825F254-F04B-4410-9D67-6BB42A1BC612'    -- Legislative District 36
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F825F254-F04B-4410-9D67-6BB42A1BC612', 'Noel Christina Frame', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F825F254-F04B-4410-9D67-6BB42A1BC612', 'Liz Berry', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F825F254-F04B-4410-9D67-6BB42A1BC612', 'Sarah Reyneveld', 1,2);

--SET @catid = 'FB880F79-88BA-4823-A7D4-A73980F81B1A'    -- Legislative District 37
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FB880F79-88BA-4823-A7D4-A73980F81B1A', 'Sharon Tomiko Santos', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FB880F79-88BA-4823-A7D4-A73980F81B1A', 'John Stafford', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FB880F79-88BA-4823-A7D4-A73980F81B1A', 'Kirsten Harris-Talley', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FB880F79-88BA-4823-A7D4-A73980F81B1A', 'Chukundi Salisbury', 1,2);

--SET @catid = 'CCFC5848-1F3A-4EE8-830D-623C1183D60A'    -- Legislative District 38
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CCFC5848-1F3A-4EE8-830D-623C1183D60A', 'June Robinson', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CCFC5848-1F3A-4EE8-830D-623C1183D60A', 'Bernard Moody', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CCFC5848-1F3A-4EE8-830D-623C1183D60A', 'Emily Wicks', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CCFC5848-1F3A-4EE8-830D-623C1183D60A', 'Bert Johnson', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CCFC5848-1F3A-4EE8-830D-623C1183D60A', 'Mike Sells', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CCFC5848-1F3A-4EE8-830D-623C1183D60A', 'David Wiley', null,2);

--SET @catid = '666E494C-D7C7-49C2-8288-5FB16049EBC4'    -- Legislative District 39
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '666E494C-D7C7-49C2-8288-5FB16049EBC4', 'Keith L. Wagoner', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '666E494C-D7C7-49C2-8288-5FB16049EBC4', 'Kathryn A. Lewandowsky', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '666E494C-D7C7-49C2-8288-5FB16049EBC4', 'Robert J. Sutherland', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '666E494C-D7C7-49C2-8288-5FB16049EBC4', 'Claus Joens', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','666E494C-D7C7-49C2-8288-5FB16049EBC4', 'Carolyn Eslick', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '666E494C-D7C7-49C2-8288-5FB16049EBC4', 'Ryan Johnson', 1,2);

--SET @catid = '5DCD5262-3A30-4DDA-A8C3-AAD5DF3444D9'    -- Legislative District 40
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '5DCD5262-3A30-4DDA-A8C3-AAD5DF3444D9' , 'Elizabeth Lovelett', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '5DCD5262-3A30-4DDA-A8C3-AAD5DF3444D9' , 'Charles Carrell', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '5DCD5262-3A30-4DDA-A8C3-AAD5DF3444D9' , 'Debra Lekanoff', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '5DCD5262-3A30-4DDA-A8C3-AAD5DF3444D9' , 'Alex Ramel', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '5DCD5262-3A30-4DDA-A8C3-AAD5DF3444D9' , 'Russ Dzialo', 2,2);

--SET @catid = '4359DC0A-011E-48D3-9A1C-0AF54812457C'    -- Legislative District 41
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4359DC0A-011E-48D3-9A1C-0AF54812457C', 'Lisa Wellman', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4359DC0A-011E-48D3-9A1C-0AF54812457C', 'Mike Nykreim', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4359DC0A-011E-48D3-9A1C-0AF54812457C', 'Tana Senn', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4359DC0A-011E-48D3-9A1C-0AF54812457C', 'My-Linh Thai', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4359DC0A-011E-48D3-9A1C-0AF54812457C', 'Al Rosenthal', 2,2);

--SET @catid = '57592DEB-EDF9-4F37-AEDD-89F41968D464'    -- Legislative District 42
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '57592DEB-EDF9-4F37-AEDD-89F41968D464', 'Luanne Van Werven', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '57592DEB-EDF9-4F37-AEDD-89F41968D464', 'Alice Rule', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '57592DEB-EDF9-4F37-AEDD-89F41968D464', 'Jennifer Sefzik', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '57592DEB-EDF9-4F37-AEDD-89F41968D464', 'Sharon Shewmake', 1,2);

--SET @catid = '6719E611-104B-4605-B16D-96DE62D9E5E1'    -- Legislative District 43
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6719E611-104B-4605-B16D-96DE62D9E5E1', 'Nicole Macri', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6719E611-104B-4605-B16D-96DE62D9E5E1', 'Leslie Klein', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6719E611-104B-4605-B16D-96DE62D9E5E1', 'Frank Chopp', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6719E611-104B-4605-B16D-96DE62D9E5E1', 'Sherae Lascelles', 2,2);

--SET @catid = '0B56CDFA-4930-4427-A132-887F3C789C71'    -- Legislative District 44
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '0B56CDFA-4930-4427-A132-887F3C789C71', 'John Lovick', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '0B56CDFA-4930-4427-A132-887F3C789C71', 'John T. Kartak', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '0B56CDFA-4930-4427-A132-887F3C789C71', 'Mark A. James', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '0B56CDFA-4930-4427-A132-887F3C789C71', 'April Berg', 1,2);

--SET @catid = 'D6FF789D-8476-4079-85C1-1324F6FDA740'    -- Legislative District 45
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D6FF789D-8476-4079-85C1-1324F6FDA740', 'Roger Goodman', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D6FF789D-8476-4079-85C1-1324F6FDA740', 'John P. Gibbons', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D6FF789D-8476-4079-85C1-1324F6FDA740', 'Larry Springer', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D6FF789D-8476-4079-85C1-1324F6FDA740', 'Amber Krabach', 2,2);

--SET @catid = '1986C94B-3384-4716-B9ED-E6752D8230A5'    -- Legislative District 46
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1986C94B-3384-4716-B9ED-E6752D8230A5', 'Gerry Pollet', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1986C94B-3384-4716-B9ED-E6752D8230A5', 'Eric J. Brown', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1986C94B-3384-4716-B9ED-E6752D8230A5', 'Javier Valdez', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1986C94B-3384-4716-B9ED-E6752D8230A5', 'Beth Daranciang', 2,2);

--SET @catid = '8E28DC52-ED49-4CDC-9FE2-77BA4E754E48'    -- Legislative District 47
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8E28DC52-ED49-4CDC-9FE2-77BA4E754E48', 'Debra Entenman', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8E28DC52-ED49-4CDC-9FE2-77BA4E754E48', 'Kyle Lyebyedyev', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8E28DC52-ED49-4CDC-9FE2-77BA4E754E48', 'Pat Sullivan', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8E28DC52-ED49-4CDC-9FE2-77BA4E754E48', 'Ted Cooke', 2,2);

--SET @catid = '9A3DF9D3-A0E4-4E37-84D3-442A18F4CC25'    -- Legislative District 48
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9A3DF9D3-A0E4-4E37-84D3-442A18F4CC25', 'Vandana Slatter', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9A3DF9D3-A0E4-4E37-84D3-442A18F4CC25', 'Victor H. Bishop', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9A3DF9D3-A0E4-4E37-84D3-442A18F4CC25', 'Amy Walen', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9A3DF9D3-A0E4-4E37-84D3-442A18F4CC25', 'Tim J. Hickey', 2,2);

--SET @catid = '3085C76F-74BE-419C-9D79-9500955B6CEF'    -- Legislative District 49
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3085C76F-74BE-419C-9D79-9500955B6CEF', 'Annette Cleveland', 1,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3085C76F-74BE-419C-9D79-9500955B6CEF', 'Rey Reynolds', 2,1);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3085C76F-74BE-419C-9D79-9500955B6CEF', 'Sharon Wylie', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3085C76F-74BE-419C-9D79-9500955B6CEF', 'Justin Forsman', 2,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3085C76F-74BE-419C-9D79-9500955B6CEF', 'Monica Stonier', 1,2);
Insert into Ticket (Id, ElectionId, CategoryId, Description, partyId, TicketType) values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3085C76F-74BE-419C-9D79-9500955B6CEF', 'Park Llafet', 2,2);

-- judicial
--SET @catid = 'D1385E48-9803-4753-B925-3AE36376113E'    -- cat76
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D1385E48-9803-4753-B925-3AE36376113E' , 'Dave Larson');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D1385E48-9803-4753-B925-3AE36376113E' , 'Raquel Montoya-Lewis');
--SET @catid = '2DF69364-F2CA-4733-9B8D-9A81A57F6698'    -- cat77
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2DF69364-F2CA-4733-9B8D-9A81A57F6698', 'Charles W. Johnson');
--SET @catid = 'E50D3BD4-87FB-49A4-8605-1C0123846905'    -- cat78
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E50D3BD4-87FB-49A4-8605-1C0123846905', 'Richard S. Serns');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E50D3BD4-87FB-49A4-8605-1C0123846905', 'G. Helen Whitener');
--SET @catid = '4615A8AE-678F-472A-AB8D-116F776AD47E'    -- cat79
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '4615A8AE-678F-472A-AB8D-116F776AD47E', 'Debra L. Stephens');
--SET @catid = '9DC2D1CC-0C3D-45F6-AC21-3F1F9B564CAD'    -- cat80  Court of Appeals
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','9DC2D1CC-0C3D-45F6-AC21-3F1F9B564CAD' , 'David S. Mann');
--SET @catid = '52780E2E-DC1C-4610-BBBE-DAAD451C14CC'    -- cat81
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '52780E2E-DC1C-4610-BBBE-DAAD451C14CC', 'Bill Bowman');
--SET @catid = 'DBEE79DF-090A-4BBD-BDC1-CA1E26EF93C4'    -- cat82
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'DBEE79DF-090A-4BBD-BDC1-CA1E26EF93C4' , 'Linda Coburn');
--SET @catid = '57A02606-B364-435C-B235-D6C8CFC2D5A8'    -- cat83
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '57A02606-B364-435C-B235-D6C8CFC2D5A8', 'Lisa Worswick');
--SET @catid = '6FAEB3E1-96A9-4087-A193-87CA47585528'    -- cat84
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6FAEB3E1-96A9-4087-A193-87CA47585528', 'Lisa L. Sutton');
--SET @catid = '6D3BF1BC-E842-43B6-B354-AA9BB76A00B8'    -- cat85
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6D3BF1BC-E842-43B6-B354-AA9BB76A00B8', 'Marshall Casey');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6D3BF1BC-E842-43B6-B354-AA9BB76A00B8', 'Tracy Arlene Staab');
--SET @catid = 'CF6EDB6E-D27F-4E30-9406-36EE3DAB9000'    -- cat86
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CF6EDB6E-D27F-4E30-9406-36EE3DAB9000', 'Rebecca Pennell');
--SET @catid = 'A8B4E3A6-ED63-4532-A0A7-AAAF45F6D202'    -- cat87  Adams Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A8B4E3A6-ED63-4532-A0A7-AAAF45F6D202', 'Steve Dixon');
--SET @catid = '13E22951-5111-4DC0-BEAE-D90893C4266B'    -- cat88  Asotin, Columbia
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '13E22951-5111-4DC0-BEAE-D90893C4266B', 'Brook J. Burns');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '13E22951-5111-4DC0-BEAE-D90893C4266B', 'G. Scott Marinella');
--SET @catid = '3DCDCB4A-F5D5-43FC-90A8-3FB40C17E82A'    -- cat89  Benton, Franklin Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3DCDCB4A-F5D5-43FC-90A8-3FB40C17E82A', 'Dave Peterson');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3DCDCB4A-F5D5-43FC-90A8-3FB40C17E82A', 'Sharon Brown');
--SET @catid = '5FC9EC2E-7EEE-44FB-9D75-39FB523EAE04'    -- cat90
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '5FC9EC2E-7EEE-44FB-9D75-39FB523EAE04' , 'Joe Burrowes');
--SET @catid = '6875B187-7301-463D-95A0-B11A947E27D7'    -- cat91
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6875B187-7301-463D-95A0-B11A947E27D7', 'Alexander Carl Ekstrom');
--SET @catid = 'B257F910-E196-4C98-93F5-1CD6A97F15B7'    -- cat92
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'B257F910-E196-4C98-93F5-1CD6A97F15B7', 'Cameron Mitchell');
--SET @catid = '71FDE414-3214-4E08-B997-F60D8F84306B'    -- cat93
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '71FDE414-3214-4E08-B997-F60D8F84306B' , 'Sam Swanberg');
--SET @catid = 'C37795EE-AFC1-4A9C-A6A9-89BEDAB513AD'    -- cat94
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C37795EE-AFC1-4A9C-A6A9-89BEDAB513AD', 'Carrie Runge');
--SET @catid = '8FBBF357-76BB-40BE-8DFB-194CA6FFF0D8'    -- cat95
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '8FBBF357-76BB-40BE-8DFB-194CA6FFF0D8', 'Jacqueline Shea Brown');
--SET @catid = 'AC9372F8-86C1-43A1-8094-D0DB5479FC80'    -- cat96  Chelan
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'AC9372F8-86C1-43A1-8094-D0DB5479FC80', 'Lesley A. Allan');
--SET @catid = '917D2575-5F0E-4F1B-9E9F-F9318482C0DC'    -- cat97
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '917D2575-5F0E-4F1B-9E9F-F9318482C0DC', 'Travis Brandt');
--SET @catid = 'D90683BD-E239-4556-A592-EDC6A1DC8242'    -- cat98
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D90683BD-E239-4556-A592-EDC6A1DC8242', 'Kristin Ferra');
--SET @catid = '75F11078-8071-4685-98F9-9AAA200C0FDE'    -- cat99  Clallam
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '75F11078-8071-4685-98F9-9AAA200C0FDE', 'Lauren Erickson');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '75F11078-8071-4685-98F9-9AAA200C0FDE', 'Lisa N. Dublin');
--SET @catid = '85B4CE0B-12C0-4998-97CF-5A70C7552D3F'    -- cat100
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '85B4CE0B-12C0-4998-97CF-5A70C7552D3F', 'Simon Barnhart');
--SET @catid = 'F043F886-D2BC-447B-9B99-9DB7147E1E00'    -- cat101
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F043F886-D2BC-447B-9B99-9DB7147E1E00', 'Brent Basden');
--SET @catid = '303BE419-F3C7-4DB8-9396-4DF15322365F'    -- cat102  Douglas
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '303BE419-F3C7-4DB8-9396-4DF15322365F', 'Brian C. Huber');
--SET @catid = '2DDFCC8A-D54E-4B43-BB40-9C392260F876'    -- cat103  Ferry, Pend Oriville
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2DDFCC8A-D54E-4B43-BB40-9C392260F876', 'Patrick A. Monasmith');
--SET @catid = '3EDC2EDA-18BE-41C1-B062-CC816E63280A'    -- cat104
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3EDC2EDA-18BE-41C1-B062-CC816E63280A', 'Jessica Taylor Reeves');
--SET @catid = 'C4738955-D00A-4713-AD9E-EF604F20FEFE'    -- cat105  Grant Superior court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C4738955-D00A-4713-AD9E-EF604F20FEFE', 'David Estudillo');
--SET @catid = '325273D6-90F7-4F84-8ECD-FB163523E03E'    -- cat106
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '325273D6-90F7-4F84-8ECD-FB163523E03E', 'John D. Knodell');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '325273D6-90F7-4F84-8ECD-FB163523E03E', 'Kevin J. McCrae');
--SET @catid = 'CBFFBE19-5C16-4633-AF0C-076843A751D0'    -- cat107
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CBFFBE19-5C16-4633-AF0C-076843A751D0', 'John Antosz');
--SET @catid = 'FECFCCED-7ECF-4343-A3D2-157493B6842A'    -- cat108  Grays Harbor
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'FECFCCED-7ECF-4343-A3D2-157493B6842A', 'Katie Svoboda');
--SET @catid = 'D7DC0FA1-6420-4C53-80CA-3A6781C4C79E'    -- cat109
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'D7DC0FA1-6420-4C53-80CA-3A6781C4C79E', 'David L. Edwards');
--SET @catid = '64659BDD-8FDE-4C40-A6C3-7AB639D96336'    -- cat110
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '64659BDD-8FDE-4C40-A6C3-7AB639D96336', 'David Mistachkin');
--SET @catid = '88300B76-31DF-4B4E-AB1D-E892A88DC463'    -- cat111  Island
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '88300B76-31DF-4B4E-AB1D-E892A88DC463', 'Christon C. Skinner');
--SET @catid = '10F8BCF9-7763-4DC3-B00A-4FE860643AA1'    -- cat112
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '10F8BCF9-7763-4DC3-B00A-4FE860643AA1', 'Kathleen Petrich');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '10F8BCF9-7763-4DC3-B00A-4FE860643AA1', 'Carolyn Cliff');
--SET @catid = '36C039A8-EBDD-4DAA-8C07-E621C35A1500'    -- cat113  Jefferson
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '36C039A8-EBDD-4DAA-8C07-E621C35A1500', 'Keith C. Harper');
--SET @catid = '400D5F5C-6847-4274-991F-2A5F2F350714'    -- cat114  King Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '400D5F5C-6847-4274-991F-2A5F2F350714', 'Hillary Madsen');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '400D5F5C-6847-4274-991F-2A5F2F350714', 'Andrea Robertson');
--SET @catid = '3F385BB9-99CB-4E08-BE9E-9C543206AED2'    -- cat115
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3F385BB9-99CB-4E08-BE9E-9C543206AED2', 'Doug North');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '3F385BB9-99CB-4E08-BE9E-9C543206AED2', 'Carolyn Ladd');
--SET @catid = '51E61D09-155E-4899-99E3-58D1688D9E41'    -- cat116  Kitsap
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '51E61D09-155E-4899-99E3-58D1688D9E41' , 'Tina Robinson');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '51E61D09-155E-4899-99E3-58D1688D9E41' , 'Lynn Fleischbein');
--SET @catid = '9C573249-EBBB-4E14-84E4-EBD7A384FA13'    -- cat117 Kittitas
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9C573249-EBBB-4E14-84E4-EBD7A384FA13', 'Candace Hooper');
--SET @catid = '6CBF1121-1433-4D1E-9E60-E01E654A76DE'    -- cat118
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '6CBF1121-1433-4D1E-9E60-E01E654A76DE', 'Scott R. Sparks');
--SET @catid = 'F311D6F3-FB68-4242-8942-9F8CCF9FB124'    -- cat119  Klickitat
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'F311D6F3-FB68-4242-8942-9F8CCF9FB124', 'Randall Krog');
--SET @catid = 'C4D9613C-04F6-4E8A-8F56-DABE9B67A867'    -- cat120  Lewis Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C4D9613C-04F6-4E8A-8F56-DABE9B67A867', 'J. Andrew Toynbee');
--SET @catid = '9900BAF5-497D-433C-9BC0-99A085315C98'    -- cat121
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '9900BAF5-497D-433C-9BC0-99A085315C98', 'James W. Lawler');
--SET @catid = '2FD9FD9D-79A7-4004-B1A7-74912D5D044F'    -- cat122
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '2FD9FD9D-79A7-4004-B1A7-74912D5D044F', 'Joely ORourke');
--SET @catid = 'E03341CA-7059-440B-913D-7CF0F423F79D'    -- cat123  Lincoln
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E03341CA-7059-440B-913D-7CF0F423F79D', 'Jeff Barkdull');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E03341CA-7059-440B-913D-7CF0F423F79D', 'Dan B. Johnson');
--SET @catid = '21DF405C-75B9-49B1-9733-7A974AC99945'    -- cat124  Mason Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '21DF405C-75B9-49B1-9733-7A974AC99945', 'Amber Finlay');
--SET @catid = 'DD9251F6-6D90-4E00-9AD2-A717A60F0F8E'    -- cat125
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','DD9251F6-6D90-4E00-9AD2-A717A60F0F8E', 'Monty D. Cobb');
--SET @catid = '1D0E3106-D611-4FE9-8F42-B7E725F8383F'    -- cat126
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '1D0E3106-D611-4FE9-8F42-B7E725F8383F', 'Daniel Goodell');
--SET @catid = '4CE99A7D-A009-4AED-A6A4-F39E38018CE3'    -- cat127  Okanogan
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6','4CE99A7D-A009-4AED-A6A4-F39E38018CE3', 'Henry Hank Rawson');
--SET @catid = '096DA2A0-6C07-4611-AC8C-3A0B0A9CE07C'    -- cat128  
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '096DA2A0-6C07-4611-AC8C-3A0B0A9CE07C', 'Chris Culp');
--SET @catid = 'CA02E3CD-7F2A-464A-8C72-AB3D000A0635'    -- cat129  Pacific
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'CA02E3CD-7F2A-464A-8C72-AB3D000A0635', 'Donald J. Richter');
--SET @catid = '16B8C5FD-99D4-47CC-8487-7E2EE792BDC9'    -- cat130  Pierce
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '16B8C5FD-99D4-47CC-8487-7E2EE792BDC9', 'Brad J. Horenstein');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '16B8C5FD-99D4-47CC-8487-7E2EE792BDC9', 'Bryan Chushcoff');
--SET @catid = '10D26CAA-3442-4EF8-9415-3D4E8D170681'    -- cat131  San Juan
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '10D26CAA-3442-4EF8-9415-3D4E8D170681', 'Kathryn C. Loring');
--SET @catid = '436F0897-B812-45E5-A09F-E9F70F09A591'    -- cat132  Skagit
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '436F0897-B812-45E5-A09F-E9F70F09A591', 'Tom Seguine');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '436F0897-B812-45E5-A09F-E9F70F09A591', 'Elizabeth Yost Neidzwski');
--SET @catid = '51843242-AAD2-4EA1-A540-C54D3DC3B2CA'    -- cat133 Snohomish
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '51843242-AAD2-4EA1-A540-C54D3DC3B2CA', 'Robert Grant');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '51843242-AAD2-4EA1-A540-C54D3DC3B2CA', 'Cassandra Lopez Shaw');
--SET @catid = 'E8EFCB4B-871D-49FF-A0BD-D003004E7C99'    -- cat134  Thurston Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E8EFCB4B-871D-49FF-A0BD-D003004E7C99', 'Sharonda D. Amamilo');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'E8EFCB4B-871D-49FF-A0BD-D003004E7C99', 'Scott Ahlf');
--SET @catid = 'EC4B58DD-AACE-4ACC-A2F6-C846DA3E1A33'    -- cat135  Walla Walla Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'EC4B58DD-AACE-4ACC-A2F6-C846DA3E1A33', 'Michael S. Mitchell');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'EC4B58DD-AACE-4ACC-A2F6-C846DA3E1A33', 'Brandon L. Johnson');
--SET @catid = '5E12BF1F-537B-4D9D-8947-868B7D6C6A83'    -- cat136
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '5E12BF1F-537B-4D9D-8947-868B7D6C6A83', 'M. Scott Wolfram');
--SET @catid = 'A9AEF769-6B11-4B7B-8ECC-5A799148CFA9'    -- cat137 Whatcom Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A9AEF769-6B11-4B7B-8ECC-5A799148CFA9', 'Evan Jones');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'A9AEF769-6B11-4B7B-8ECC-5A799148CFA9', 'James Erb');
--SET @catid = '0F1EEFCF-4508-439E-9D0F-ADE8838C588B'    -- cat138
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '0F1EEFCF-4508-439E-9D0F-ADE8838C588B', 'David E. Freeman');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '0F1EEFCF-4508-439E-9D0F-ADE8838C588B', 'Jim Nelson');
--SET @catid = 'CFA8040B-D227-4E59-9DDC-C85FB0435965'    -- cat139 Whitman Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6',  'CFA8040B-D227-4E59-9DDC-C85FB0435965', 'Gary J. Libey');
--SET @catid = 'C8326398-88E8-4ECA-BA02-E8DF9CA6EFBC'    -- cat140  Yakima Superior Court
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C8326398-88E8-4ECA-BA02-E8DF9CA6EFBC', 'Elsabeth Tutsch');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', 'C8326398-88E8-4ECA-BA02-E8DF9CA6EFBC', 'Bronson Faul');
--SET @catid = '17857B11-846A-4CF0-82A0-29102992A39C'    -- cat141
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '17857B11-846A-4CF0-82A0-29102992A39C', 'Doublas L. Federspiel');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '17857B11-846A-4CF0-82A0-29102992A39C', 'Jeff Swan');
--SET @catid = '11C55A61-0C7D-4778-AA66-4110F95D79CF'    -- cat142
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '11C55A61-0C7D-4778-AA66-4110F95D79CF', 'Scott Bruns');
Insert into Ticket (Id, ElectionId, CategoryId, Description)values (UUID_GENERATE_V4(), 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6', '11C55A61-0C7D-4778-AA66-4110F95D79CF', 'Blaine Grorge Gibson');


