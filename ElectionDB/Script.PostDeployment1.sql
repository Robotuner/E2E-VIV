/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

declare @electionId uniqueIdentifier = 'A13ACD4A-D415-4B27-AFE6-E2310AC71BC6'

Insert into Election (Id, Date, StartDateLocal, EndDateLocal, Description, Version) values (@electionId, '2021-11-3', '2021-02-20 09:00:00', '2021-11-03 20:00:00', 'US Presidential Election', '1.0.0.0')
declare @measureId int = 1
declare @federalId int = 2
declare @stateId int = 3
declare @legislativeId int = 4
declare @judicialId int = 5

Insert into CategoryType (Description) values ('Measure')
Insert into CategoryType (Description) values ('Federal')
Insert into CategoryType (Description) values ('State')
Insert into CategoryType (Description) values ('Legislative')
Insert into CategoryType (Description) values ('Judicial')

declare @cat1 UNIQUEIDENTIFIER = '658F3CDB-FBE2-44EC-B258-6811FA63271C'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, SubTitle, Information, Sequence) values (@cat1, @electionId, @measureId, 'Referendum Measure', 'Statewide - Referendum Measure No. 90',
 null,
 'The legislature passed Engrossed Substitute Senate Bill 5395 concerning comprehensive sexual health education. This bill would require school districts to adopt or develop, consistent with state standards, comprehensive age-appropriate sexual health education, as defined, for all students, and excuse students if their parents request.', 1)
 declare @cat2 UNIQUEIDENTIFIER = '3D67CFD2-2645-44EB-8196-02E729875D0B'
insert into Category (Id, ElectionId, CategoryTypeId, Heading, Title, SubTitle, Information, Sequence) values (@cat2,@electionId, @measureId, 'Advisory Vote', 'Statewide - Referendum Measure No. 32',
 'Engrossed Substitute Senate Bill 5323', 'The legislature imposed, without a vote of the people, a retail sales tax on pass-through charges retail establishments collect for specified carryout bags, costing $32,000,000 in its first ten years, for government spending.',2)
 declare @cat3 UNIQUEIDENTIFIER = '6AF40813-D42C-4DA0-A8E0-FE61D922C680'
insert into Category (Id, ElectionId, CategoryTypeId, Heading, Title, SubTitle, Information, Sequence) values (@cat3, @electionId, @measureId, 'Advisory Vote', 'Statewide - Referendum Measure No. 33', 
 'Substitute Senate Bill 5628', 'The legislature imposed, without a vote of the people, a tax on heavy equipment rentals to consumers by heavy equipment rental property dealers, costing $103,000,000 in its first ten years, for government spending.',3)
 declare @cat4 UNIQUEIDENTIFIER = '716C38A8-6CA2-427E-9FEC-763B997F3C54'
insert into Category (Id, ElectionId, CategoryTypeId, Heading, Title, SubTitle, Information, Sequence) values (@cat4,@electionId, @measureId, 'Advisory Vote', 'Statewide - Referendum Measure No. 34',
 'Engrossed Substitute Senate Bill 6492', 'The legislature increased, without a vote of the people, the business and occupation tax rate for certain businesses, while reducing certain surcharges, costing $843,000,000 in its first ten years, for government spending.',4)
 declare @cat5 UNIQUEIDENTIFIER = '9650C476-C579-4D4D-887D-9C154A95239B'
insert into Category (Id, ElectionId, CategoryTypeId, Heading, Title, SubTitle, Information, Sequence) values (@cat5, @electionId, @measureId, 'Advisory Vote', 'Statewide - Referendum Measure No. 35',
 'Engrossed Senate Bill 6690', 'The legislature increased, without a vote of the people, the business and occupation tax on manufacturers of commercial airplanes, including components or tooling, costing $1,024,000,000 in its first ten years, for government spending.',5)
  declare @cat6 UNIQUEIDENTIFIER = '83F84069-0DD1-4E13-833B-737E85F64CFF'
insert into Category (Id, ElectionId, CategoryTypeId, Heading, Title, SubTitle, Information, Sequence) values (@cat6,@electionId, @measureId, 'Senate Joint Resolution', 'Statewide - Engrossed Senate Joint Resolution No. 8212',
 'Measure Text', 'The legislature has proposed a constitutional amendment on investment of public funds. This amendment would allow public money held in a fund for long-term care services and supports to be invested by governments as authorized by state law, including investments in private stocks.',6)

 declare @cat7 UNIQUEIDENTIFIER = '90545980-C4BC-48D7-BE0A-97730AAD3972'
insert into Category (Id, ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat7, @electionId, @federalId, 'Statewide', 'President/Vice President',1)
declare @cat8 UNIQUEIDENTIFIER = 'D8B38531-E9C0-4B29-A368-4791DA8090F3'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat8, @electionId, @federalId, 'Congressional District 1', 'U.S. Representative',2)
declare @cat9 UNIQUEIDENTIFIER = '667BD869-D334-48E7-8C20-827B5F6983BC'
insert into Category (id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat9,@electionId, @federalId, 'Congressional District 2', 'U.S. Representative',3)
declare @cat10 UNIQUEIDENTIFIER = '70EFE57B-FCB0-46C0-8559-230B8D6C301E'
insert into Category (id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat10,@electionId, @federalId, 'Congressional District 3', 'U.S. Representative',4)
declare @cat11 UNIQUEIDENTIFIER = '0BFF8D66-AA56-4A99-9FFE-DFABF4A0A6C9'
insert into Category (id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat11,@electionId, @federalId, 'Congressional District 4', 'U.S. Representative',5)
declare @cat12 UNIQUEIDENTIFIER = '8DBCD15C-B7BF-4E25-B5FA-EF2B9300FA0D'
insert into Category (id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat12,@electionId, @federalId, 'Congressional District 5', 'U.S. Representative',6)
declare @cat13 UNIQUEIDENTIFIER = '29673FC3-CFC9-47D8-87F6-6389E6089FB3'
insert into Category (id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat13,@electionId, @federalId, 'Congressional District 6', 'U.S. Representative',7)
declare @cat14 UNIQUEIDENTIFIER = '95B46107-D6BB-4EDF-8D5D-87C648FCE7DB'
insert into Category (id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat14,@electionId, @federalId, 'Congressional District 7', 'U.S. Representative',8)
declare @cat15 UNIQUEIDENTIFIER = '05B6D9DC-73EE-4E1C-A352-AB6FBF91D208'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat15,@electionId, @federalId, 'Congressional District 8', 'U.S. Representative',9)
declare @cat16 UNIQUEIDENTIFIER = 'BD29F494-E223-47CE-8385-ED98CD096ABE'
insert into Category (id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat16,@electionId, @federalId, 'Congressional District 9', 'U.S. Representative',10)
declare @cat17 UNIQUEIDENTIFIER = 'DE7F37F2-C9DB-45B6-A5FA-DFFE67EB394A'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat17,@electionId, @federalId, 'Congressional District 10', 'U.S. Representative',11)
declare @cat18 UNIQUEIDENTIFIER = '383F37D2-4613-4CF2-A83C-7E004251CA2F'
insert into Category (id, ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat18,@electionId, @stateId, 'Statewide', 'Governor',1)
declare @cat19 UNIQUEIDENTIFIER = '5AD15A1E-593E-45FE-B149-BE43B91FC82B'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat19,@electionId, @stateId, 'Statewide', 'Lt. Governor',2)
declare @cat20 UNIQUEIDENTIFIER = '6383E314-2111-45DE-A0E4-908AB83E97D5'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat20,@electionId, @stateId, 'Statewide', 'Secretary Of State',3)
declare @cat21 UNIQUEIDENTIFIER = '9B4E9F03-E39F-4BB5-8838-F6137EF391C1'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat21,@electionId, @stateId, 'Statewide', 'State Treasurer',4)
declare @cat22 UNIQUEIDENTIFIER = 'C33EC949-6CE7-4F5B-A863-B3EE792247EC'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat22,@electionId, @stateId, 'Statewide', 'State Auditor',5)
declare @cat23 UNIQUEIDENTIFIER = '3C54BD34-AB8A-4F13-810F-F128DAA8766E'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat23,@electionId, @stateId, 'Statewide', 'Attorney General',6)
declare @cat24 UNIQUEIDENTIFIER = '4E796214-2FD1-4177-9F1D-24BA13BAAE43'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat24,@electionId, @stateId, 'Statewide', 'Commissioner of Public Lands',7)
declare @cat25 UNIQUEIDENTIFIER = '3FE4CFD6-526C-4D6A-AF56-E12EAE745ABC'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat25,@electionId, @stateId, 'Statewide', 'Superintendent of Public Instruction',8)
declare @cat26 UNIQUEIDENTIFIER = 'A983A6BB-98C0-44B6-A3CD-4E8E38BBF165'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat26,@electionId, @stateId, 'Statewide', 'Insurance Commissioner',9)

declare @cat27 UNIQUEIDENTIFIER = 'E5CAB96F-D5C8-4835-9313-FA503E3C44CC'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat27,@electionId, @legislativeId, 'Legislative', 'Legislative District 1',1)
declare @cat28 UNIQUEIDENTIFIER = '75AC1826-6092-4E79-B3A6-662AEB557FCB'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat28,@electionId, @legislativeId, 'Legislative', 'Legislative District 2',2)
declare @cat29 UNIQUEIDENTIFIER = 'A2ADDDB5-92EB-43E0-8C87-6F9FBA5DAAA3'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat29,@electionId, @legislativeId, 'Legislative', 'Legislative District 3',3)
declare @cat30 UNIQUEIDENTIFIER = '77DA07AA-A491-4426-B4DA-9B9843C754A1'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat30,@electionId, @legislativeId, 'Legislative', 'Legislative District 4',4)
declare @cat31 UNIQUEIDENTIFIER = '538F1962-C23B-4004-8561-CEAD17639EAA'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat31,@electionId, @legislativeId, 'Legislative', 'Legislative District 5',5)
declare @cat32 UNIQUEIDENTIFIER = '29313286-0E18-4D70-ACB6-549EF8421B5B'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat32,@electionId, @legislativeId, 'Legislative', 'Legislative District 6',6)
declare @cat33 UNIQUEIDENTIFIER = '7B5026A3-D8AA-4EB2-84DD-E7B4A58199E3'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat33,@electionId, @legislativeId, 'Legislative', 'Legislative District 7',7)
declare @cat34 UNIQUEIDENTIFIER = 'A31CC2C5-5AEE-497C-83B4-ADF7AC27306C'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat34,@electionId, @legislativeId, 'Legislative', 'Legislative District 8',8)
declare @cat35 UNIQUEIDENTIFIER = 'BABE5E86-5342-4A59-A2FD-F13CE8DB3FAA'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat35,@electionId, @legislativeId, 'Legislative', 'Legislative District 9',9)
declare @cat36 UNIQUEIDENTIFIER = '2D65E8AA-899B-4DDD-8710-60AEBD0CCE01'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat36,@electionId, @legislativeId, 'Legislative', 'Legislative District 10',1)
declare @cat37 UNIQUEIDENTIFIER = 'D6CB6AFD-6802-4061-A244-A0F8F2A97541'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat37,@electionId, @legislativeId, 'Legislative', 'Legislative District 11',2)
declare @cat38 UNIQUEIDENTIFIER = '4ED33B65-B9BD-4FEA-9B53-96ADBB2F75E5'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat38,@electionId, @legislativeId, 'Legislative', 'Legislative District 12',3)
declare @cat39 UNIQUEIDENTIFIER = '60E04F71-C117-452C-A20B-A74E44A95F40'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat39,@electionId, @legislativeId, 'Legislative', 'Legislative District 13',4)
declare @cat40 UNIQUEIDENTIFIER = 'F509843F-1E86-4BEF-AD2B-246DD212BC7B'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat40,@electionId, @legislativeId, 'Legislative', 'Legislative District 14',5)
declare @cat41 UNIQUEIDENTIFIER = '70D8BD8D-60F5-44D7-895B-17941C0C37C9'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat41,@electionId, @legislativeId, 'Legislative', 'Legislative District 15',6)
declare @cat42 UNIQUEIDENTIFIER = '40815FCF-B2FD-45A2-BEB1-F2CC8C87E717'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat42,@electionId, @legislativeId, 'Legislative', 'Legislative District 16',7)
declare @cat43 UNIQUEIDENTIFIER = '6693630F-1B3C-4BBA-B4FB-6C6F0F44A740'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat43,@electionId, @legislativeId, 'Legislative', 'Legislative District 17',8)
declare @cat44 UNIQUEIDENTIFIER = '41BD9562-CC7F-47FF-B3A0-D6CC682804D5'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat44,@electionId, @legislativeId, 'Legislative', 'Legislative District 18',9)
declare @cat45 UNIQUEIDENTIFIER = 'A4407F7B-2682-4E11-B0E9-87C6FB4F1830'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat45,@electionId, @legislativeId, 'Legislative', 'Legislative District 19',10)
declare @cat46 UNIQUEIDENTIFIER = 'ABC7474C-398E-4E7C-8C90-24A3BEABA05B'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat46,@electionId, @legislativeId, 'Legislative', 'Legislative District 20',11)
declare @cat47 UNIQUEIDENTIFIER = '1440BA1C-23CE-4295-A14D-C2C03CE80267'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat47,@electionId, @legislativeId, 'Legislative', 'Legislative District 21',12)
declare @cat48 UNIQUEIDENTIFIER = 'B53388ED-FCAA-4281-834E-02D5236A4457'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat48,@electionId, @legislativeId, 'Legislative', 'Legislative District 22',13)
declare @cat49 UNIQUEIDENTIFIER = 'EDBA1A3F-0427-4970-9961-7B486DDF822C'
insert into Category (ID,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat49,@electionId, @legislativeId, 'Legislative', 'Legislative District 23',14)
declare @cat50 UNIQUEIDENTIFIER = '1780068E-8145-4AB3-9786-E1956CF62E8A'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat50,@electionId, @legislativeId, 'Legislative', 'Legislative District 24',15)
declare @cat51 UNIQUEIDENTIFIER = '8466A090-EB67-423C-B619-C9F0D1BF8A11'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat51,@electionId, @legislativeId, 'Legislative', 'Legislative District 25',16)
declare @cat52 UNIQUEIDENTIFIER = '1DDBE15B-C1FF-4446-87F2-2649FA9FB3E3'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat52,@electionId, @legislativeId, 'Legislative', 'Legislative District 26',17)
declare @cat53 UNIQUEIDENTIFIER = 'C07F9B4B-8358-46AC-B2F6-7893084E6F23'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat53,@electionId, @legislativeId, 'Legislative', 'Legislative District 27',18)
declare @cat54 UNIQUEIDENTIFIER = '8FF3A9B3-998F-4377-BAF1-3D90156C93F0'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat54,@electionId, @legislativeId, 'Legislative', 'Legislative District 28',19)
declare @cat55 UNIQUEIDENTIFIER = '7A5C5348-DAB6-4DC7-8812-F000C7FAD2AB'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat55,@electionId, @legislativeId, 'Legislative', 'Legislative District 29',20)
declare @cat56 UNIQUEIDENTIFIER = '07C28152-B519-4545-A5A1-24DA1EF5C571'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat56,@electionId, @legislativeId, 'Legislative', 'Legislative District 30',21)
declare @cat57 UNIQUEIDENTIFIER = 'DB2E6C95-E19F-453D-9347-FC15281D16F3'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat57,@electionId, @legislativeId, 'Legislative', 'Legislative District 31',22)
declare @cat58 UNIQUEIDENTIFIER = '97FE1731-9E87-47CE-B054-FB6A350E8799'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat58,@electionId, @legislativeId, 'Legislative', 'Legislative District 32',23)
declare @cat59 UNIQUEIDENTIFIER = '93557631-4902-4E2F-A523-083801F454B8'
insert into Category (id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat59,@electionId, @legislativeId, 'Legislative', 'Legislative District 33',24)
declare @cat60 UNIQUEIDENTIFIER = '3BC26911-F7CB-4086-9770-4AA8E6E18DF7'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat60,@electionId, @legislativeId, 'Legislative', 'Legislative District 34',25)
declare @cat61 UNIQUEIDENTIFIER = 'FB09D766-4A37-4415-8C5E-DB5796D448EE'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat61,@electionId, @legislativeId, 'Legislative', 'Legislative District 35',26)
declare @cat62 UNIQUEIDENTIFIER = 'F825F254-F04B-4410-9D67-6BB42A1BC612'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat62,@electionId, @legislativeId, 'Legislative', 'Legislative District 36',27)
declare @cat63 UNIQUEIDENTIFIER = 'FB880F79-88BA-4823-A7D4-A73980F81B1A'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat63,@electionId, @legislativeId, 'Legislative', 'Legislative District 37',28)
declare @cat64 UNIQUEIDENTIFIER = 'CCFC5848-1F3A-4EE8-830D-623C1183D60A'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat64,@electionId, @legislativeId, 'Legislative', 'Legislative District 38',29)
declare @cat65 UNIQUEIDENTIFIER = '666E494C-D7C7-49C2-8288-5FB16049EBC4'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat65,@electionId, @legislativeId, 'Legislative', 'Legislative District 39',30)
declare @cat66 UNIQUEIDENTIFIER = '5DCD5262-3A30-4DDA-A8C3-AAD5DF3444D9'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat66,@electionId, @legislativeId, 'Legislative', 'Legislative District 40',31)
declare @cat67 UNIQUEIDENTIFIER = '4359DC0A-011E-48D3-9A1C-0AF54812457C'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat67,@electionId, @legislativeId, 'Legislative', 'Legislative District 41',32)
declare @cat68 UNIQUEIDENTIFIER = '57592DEB-EDF9-4F37-AEDD-89F41968D464'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat68,@electionId, @legislativeId, 'Legislative', 'Legislative District 42',33)
declare @cat69 UNIQUEIDENTIFIER = '6719E611-104B-4605-B16D-96DE62D9E5E1'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat69,@electionId, @legislativeId, 'Legislative', 'Legislative District 43',34)
declare @cat70 UNIQUEIDENTIFIER = '0B56CDFA-4930-4427-A132-887F3C789C71'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat70,@electionId, @legislativeId, 'Legislative', 'Legislative District 44',35)
declare @cat71 UNIQUEIDENTIFIER = 'D6FF789D-8476-4079-85C1-1324F6FDA740'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat71,@electionId, @legislativeId, 'Legislative', 'Legislative District 45',36)
declare @cat72 UNIQUEIDENTIFIER = '1986C94B-3384-4716-B9ED-E6752D8230A5'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat72,@electionId, @legislativeId, 'Legislative', 'Legislative District 46',37)
declare @cat73 UNIQUEIDENTIFIER = '8E28DC52-ED49-4CDC-9FE2-77BA4E754E48'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat73,@electionId, @legislativeId, 'Legislative', 'Legislative District 47',38)
declare @cat74 UNIQUEIDENTIFIER = '9A3DF9D3-A0E4-4E37-84D3-442A18F4CC25'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat74,@electionId, @legislativeId, 'Legislative', 'Legislative District 48',39)
declare @cat75 UNIQUEIDENTIFIER = '3085C76F-74BE-419C-9D79-9500955B6CEF'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, Title, Sequence) values (@cat75,@electionId, @legislativeId, 'Legislative', 'Legislative District 49',40)

declare @cat76 UNIQUEIDENTIFIER = 'D1385E48-9803-4753-B925-3AE36376113E'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat76,@electionId, @judicialId, 'Supreme Court', 3,1 );
declare @cat77 UNIQUEIDENTIFIER = '2DF69364-F2CA-4733-9B8D-9A81A57F6698'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat77,@electionId, @judicialId, 'Supreme Court', 4,2 );
declare @cat78 UNIQUEIDENTIFIER = 'E50D3BD4-87FB-49A4-8605-1C0123846905'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat78,@electionId, @judicialId, 'Supreme Court', 6,3 );
declare @cat79 UNIQUEIDENTIFIER = '4615A8AE-678F-472A-AB8D-116F776AD47E'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat79,@electionId, @judicialId, 'Supreme Court', 7,4);
declare @cat80 UNIQUEIDENTIFIER = '9DC2D1CC-0C3D-45F6-AC21-3F1F9B564CAD'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat80,@electionId, @judicialId, 'Court of Appeals, Division 1, District 1', 5 ,5);
declare @cat81 UNIQUEIDENTIFIER = '52780E2E-DC1C-4610-BBBE-DAAD451C14CC'
insert into Category (ID,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat81,@electionId, @judicialId, 'Court of Appeals, Division 1, District 1', 6 ,6);
declare @cat82 UNIQUEIDENTIFIER = 'DBEE79DF-090A-4BBD-BDC1-CA1E26EF93C4'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat82,@electionId, @judicialId, 'Court of Appeals, Division 1, District 2', 2 ,7);
declare @cat83 UNIQUEIDENTIFIER = '57A02606-B364-435C-B235-D6C8CFC2D5A8'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat83,@electionId, @judicialId, 'Court of Appeals, Division 2, District 1', 2 ,8);
declare @cat84 UNIQUEIDENTIFIER = '6FAEB3E1-96A9-4087-A193-87CA47585528'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat84,@electionId, @judicialId, 'Court of Appeals, Division 2, District 2', 1 ,9);
declare @cat85 UNIQUEIDENTIFIER = '6D3BF1BC-E842-43B6-B354-AA9BB76A00B8'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat85,@electionId, @judicialId, 'Court of Appeals, Division 3, District 1', 2 ,10);
declare @cat86 UNIQUEIDENTIFIER = 'CF6EDB6E-D27F-4E30-9406-36EE3DAB9000'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat86,@electionId, @judicialId, 'Court of Appeals, Division 3, District 3', 1 ,11);
declare @cat87 UNIQUEIDENTIFIER = 'A8B4E3A6-ED63-4532-A0A7-AAAF45F6D202'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat87,@electionId, @judicialId, 'Adams Superior Court', 1 ,12);
declare @cat88 UNIQUEIDENTIFIER = '13E22951-5111-4DC0-BEAE-D90893C4266B'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat88,@electionId, @judicialId, 'Asotin, Columbia Garfield Superior Court', 1 ,13);
declare @cat89 UNIQUEIDENTIFIER = '3DCDCB4A-F5D5-43FC-90A8-3FB40C17E82A'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat89,@electionId, @judicialId, 'Benton, Franklin Superior Court', 1 ,14);
declare @cat90 UNIQUEIDENTIFIER = '5FC9EC2E-7EEE-44FB-9D75-39FB523EAE04'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat90,@electionId, @judicialId, 'Benton, Franklin Superior Court', 2 ,15);
declare @cat91 UNIQUEIDENTIFIER = '6875B187-7301-463D-95A0-B11A947E27D7'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat91,@electionId, @judicialId, 'Benton, Franklin Superior Court', 3 ,16);
declare @cat92 UNIQUEIDENTIFIER = 'B257F910-E196-4C98-93F5-1CD6A97F15B7'
insert into Category (ID,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat92,@electionId, @judicialId, 'Benton, Franklin Superior Court', 4 ,17);
declare @cat93 UNIQUEIDENTIFIER = '71FDE414-3214-4E08-B997-F60D8F84306B'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat93,@electionId, @judicialId, 'Benton, Franklin Superior Court', 5 ,18);
declare @cat94 UNIQUEIDENTIFIER = 'C37795EE-AFC1-4A9C-A6A9-89BEDAB513AD'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat94,@electionId, @judicialId, 'Benton, Franklin Superior Court', 6 ,19);
declare @cat95 UNIQUEIDENTIFIER = '8FBBF357-76BB-40BE-8DFB-194CA6FFF0D8'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat95,@electionId, @judicialId, 'Benton, Franklin Superior Court', 7 ,20);
declare @cat96 UNIQUEIDENTIFIER = 'AC9372F8-86C1-43A1-8094-D0DB5479FC80'
insert into Category (ID,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat96,@electionId, @judicialId, 'Chelan Superior Court', 1 ,21);
declare @cat97 UNIQUEIDENTIFIER = '917D2575-5F0E-4F1B-9E9F-F9318482C0DC'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat97,@electionId, @judicialId, 'Chelan Superior Court', 2 ,22);
declare @cat98 UNIQUEIDENTIFIER = 'D90683BD-E239-4556-A592-EDC6A1DC8242'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat98,@electionId, @judicialId, 'Chelan Superior Court', 3 ,23);
declare @cat99 UNIQUEIDENTIFIER = '75F11078-8071-4685-98F9-9AAA200C0FDE'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat99,@electionId, @judicialId, 'Clallam Superior Court', 1 ,24);
declare @cat100 UNIQUEIDENTIFIER = '85B4CE0B-12C0-4998-97CF-5A70C7552D3F'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat100,@electionId, @judicialId, 'Clallam Superior Court', 2 ,25);
declare @cat101 UNIQUEIDENTIFIER = 'F043F886-D2BC-447B-9B99-9DB7147E1E00'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat101,@electionId, @judicialId, 'Clallam Superior Court', 3 ,26);
declare @cat102 UNIQUEIDENTIFIER = '303BE419-F3C7-4DB8-9396-4DF15322365F'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat102,@electionId, @judicialId, 'Douglas', 1 ,27);
declare @cat103 UNIQUEIDENTIFIER = '2DDFCC8A-D54E-4B43-BB40-9C392260F876'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat103,@electionId, @judicialId, 'Ferry, Pend Oreille, Stevens Superior Court', 1 ,28);
declare @cat104 UNIQUEIDENTIFIER = '3EDC2EDA-18BE-41C1-B062-CC816E63280A'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat104,@electionId, @judicialId, 'Ferry, Pend Oreille, Stevens Superior Court', 2 ,29);
declare @cat105 UNIQUEIDENTIFIER = 'C4738955-D00A-4713-AD9E-EF604F20FEFE'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat105,@electionId, @judicialId, 'Grant Superior Court', 1 ,30);
declare @cat106 UNIQUEIDENTIFIER = '325273D6-90F7-4F84-8ECD-FB163523E03E'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat106,@electionId, @judicialId, 'Grant Superior Court', 2 ,31);
declare @cat107 UNIQUEIDENTIFIER = 'CBFFBE19-5C16-4633-AF0C-076843A751D0'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat107,@electionId, @judicialId, 'Grant Superior Court', 3 ,32);
declare @cat108 UNIQUEIDENTIFIER = 'FECFCCED-7ECF-4343-A3D2-157493B6842A'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat108,@electionId, @judicialId, 'Grays Harbor Superior Court', 1 ,33);
declare @cat109 UNIQUEIDENTIFIER = 'D7DC0FA1-6420-4C53-80CA-3A6781C4C79E'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat109,@electionId, @judicialId, 'Grays Harbor Superior Court', 2 ,34);
declare @cat110 UNIQUEIDENTIFIER = '64659BDD-8FDE-4C40-A6C3-7AB639D96336'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat110,@electionId, @judicialId, 'Grays Harbor Superior Court', 3 ,35);
declare @cat111 UNIQUEIDENTIFIER = '88300B76-31DF-4B4E-AB1D-E892A88DC463'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat111,@electionId, @judicialId, 'Island Superior Court', 1 ,36);
declare @cat112 UNIQUEIDENTIFIER = '10F8BCF9-7763-4DC3-B00A-4FE860643AA1'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat112,@electionId, @judicialId, 'Island Superior Court', 2 ,37);
declare @cat113 UNIQUEIDENTIFIER = '36C039A8-EBDD-4DAA-8C07-E621C35A1500'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat113,@electionId, @judicialId, 'Jefferson Superior Court', 1 ,38);
declare @cat114 UNIQUEIDENTIFIER = '400D5F5C-6847-4274-991F-2A5F2F350714'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat114,@electionId, @judicialId, 'King Superior Court', 13 ,39);
declare @cat115 UNIQUEIDENTIFIER = '3F385BB9-99CB-4E08-BE9E-9C543206AED2'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat115,@electionId, @judicialId, 'King Superior Court', 30 ,40);
declare @cat116 UNIQUEIDENTIFIER = '51E61D09-155E-4899-99E3-58D1688D9E41'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat116,@electionId, @judicialId, 'Kitsap Superior Court', 1 ,41);
declare @cat117 UNIQUEIDENTIFIER = '9C573249-EBBB-4E14-84E4-EBD7A384FA13'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat117,@electionId, @judicialId, 'Kittitas Superior Court', 1 ,42);
declare @cat118 UNIQUEIDENTIFIER = '6CBF1121-1433-4D1E-9E60-E01E654A76DE'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat118,@electionId, @judicialId, 'Kittitas Superior Court', 2 ,43);
declare @cat119 UNIQUEIDENTIFIER = 'F311D6F3-FB68-4242-8942-9F8CCF9FB124'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat119,@electionId, @judicialId, 'Klickitat, Skamania Superior Court', 1 ,44);
declare @cat120 UNIQUEIDENTIFIER = 'C4D9613C-04F6-4E8A-8F56-DABE9B67A867'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat120,@electionId, @judicialId, 'Lewis Superior Court', 1 ,45);
declare @cat121 UNIQUEIDENTIFIER = '9900BAF5-497D-433C-9BC0-99A085315C98'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat121,@electionId, @judicialId, 'Lewis Superior Court', 2 ,46);
declare @cat122 UNIQUEIDENTIFIER = '2FD9FD9D-79A7-4004-B1A7-74912D5D044F'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat122,@electionId, @judicialId, 'Lewis Superior Court', 3 ,47);
declare @cat123 UNIQUEIDENTIFIER = 'E03341CA-7059-440B-913D-7CF0F423F79D'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat123,@electionId, @judicialId, 'Lincoln Superior Court', 1 ,48);
declare @cat124 UNIQUEIDENTIFIER = '21DF405C-75B9-49B1-9733-7A974AC99945'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat124,@electionId, @judicialId, 'Mason Superior Court', 1 ,49);
declare @cat125 UNIQUEIDENTIFIER = 'DD9251F6-6D90-4E00-9AD2-A717A60F0F8E'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat125,@electionId, @judicialId, 'Mason Superior Court', 2 ,50);
declare @cat126 UNIQUEIDENTIFIER = '1D0E3106-D611-4FE9-8F42-B7E725F8383F'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat126,@electionId, @judicialId, 'Mason Superior Court', 3 ,51);
declare @cat127 UNIQUEIDENTIFIER = '4CE99A7D-A009-4AED-A6A4-F39E38018CE3'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat127,@electionId, @judicialId, 'Okanogan Superior Court', 1 ,52);
declare @cat128 UNIQUEIDENTIFIER = '096DA2A0-6C07-4611-AC8C-3A0B0A9CE07C'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat128,@electionId, @judicialId, 'Okanogan Superior Court', 2 ,53);
declare @cat129 UNIQUEIDENTIFIER = 'CA02E3CD-7F2A-464A-8C72-AB3D000A0635'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat129,@electionId, @judicialId, 'Pacific, Wahkiakum Superior Court', 1 ,54);
declare @cat130 UNIQUEIDENTIFIER = '16B8C5FD-99D4-47CC-8487-7E2EE792BDC9'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat130,@electionId, @judicialId, 'Pierce Superior Court', 4 ,55);
declare @cat131 UNIQUEIDENTIFIER = '10D26CAA-3442-4EF8-9415-3D4E8D170681'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat131,@electionId, @judicialId, 'San Juan Superior Court', 1 ,56);
declare @cat132 UNIQUEIDENTIFIER = '436F0897-B812-45E5-A09F-E9F70F09A591'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat132,@electionId, @judicialId, 'Skagit Superior Court', 3 ,57);
declare @cat133 UNIQUEIDENTIFIER = '51843242-AAD2-4EA1-A540-C54D3DC3B2CA'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat133,@electionId, @judicialId, 'Snohomish Superior Court', 8 ,58);
declare @cat134 UNIQUEIDENTIFIER = 'E8EFCB4B-871D-49FF-A0BD-D003004E7C99'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat134,@electionId, @judicialId, 'Thurston Superior Court', 8 ,59);
declare @cat135 UNIQUEIDENTIFIER = 'EC4B58DD-AACE-4ACC-A2F6-C846DA3E1A33'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat135,@electionId, @judicialId, 'Walla Walla Superior Court', 1 ,60);
declare @cat136 UNIQUEIDENTIFIER = '5E12BF1F-537B-4D9D-8947-868B7D6C6A83'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat136,@electionId, @judicialId, 'Walla Walla Superior Court', 2 ,61);
declare @cat137 UNIQUEIDENTIFIER = 'A9AEF769-6B11-4B7B-8ECC-5A799148CFA9'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat137,@electionId, @judicialId, 'Whatcom Superior Court', 2 ,62);
declare @cat138 UNIQUEIDENTIFIER = '0F1EEFCF-4508-439E-9D0F-ADE8838C588B'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat138,@electionId, @judicialId, 'Whatcom Superior Court', 4 ,63);
declare @cat139 UNIQUEIDENTIFIER = 'CFA8040B-D227-4E59-9DDC-C85FB0435965'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat139,@electionId, @judicialId, 'Whitman Superior Court', 1 ,64);
declare @cat140 UNIQUEIDENTIFIER = 'C8326398-88E8-4ECA-BA02-E8DF9CA6EFBC'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat140,@electionId, @judicialId, 'Yakima Superior Court', 2 ,65);
declare @cat141 UNIQUEIDENTIFIER = '17857B11-846A-4CF0-82A0-29102992A39C'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat141,@electionId, @judicialId, 'Yakima Superior Court', 3 ,66);
declare @cat142 UNIQUEIDENTIFIER = '11C55A61-0C7D-4778-AA66-4110F95D79CF'
insert into Category (Id,ElectionId, CategoryTypeId, Heading, JudgePosition, Sequence) values (@cat142,@electionId, @judicialId, 'Yakima Superior Court', 4 ,67);

insert into Party (Description, Active) values ('Democratic Party', 1);
insert into Party (Description, Active) values ('Republican Party', 1);
insert into Party (Description, Active) values ('Libertarian Party', 1);
insert into Party (Description, Active) values ('Green Party', 1);
insert into Party (Description, Active) values ('Socialism and Liberation Party', 1);
insert into Party (Description, Active) values ('Socialist Workers Party', 1);

