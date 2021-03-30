--DROP SCHEMA public cascade;
--CREATE SCHEMA public;
--GRANT ALL ON SCHEMA public TO postgres;
--GRANT ALL ON SCHEMA public TO public;

--DROP DATABASE IF EXISTS Election

--CREATE DATABASE Election


CREATE TABLE public.Ballot
(
    Id uuid NOT NULL,
    BallotChain text COLLATE pg_catalog.default NOT NULL,
    Electionid uuid NOT NULL,
	Nonce int NOT NULL,
    CreateDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Ballot_pkey PRIMARY KEY (Id)
)
TABLESPACE pg_default;

ALTER TABLE public.Ballot
    OWNER to postgres;
	
CREATE TABLE public.Category
(
    Id uuid NOT NULL,
    ElectionId uuid NOT NULL,
    CategoryTypeId integer NOT NULL,
    Heading text COLLATE pg_catalog.default NOT NULL,
    Title text COLLATE pg_catalog.default,
    JudgePosition integer NOT NULL DEFAULT 0,
    Information text COLLATE pg_catalog.default,
    SubTitle text COLLATE pg_catalog.default,
    CreateDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastUpdated timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Sequence integer NOT NULL DEFAULT 0,
    Selection uuid,
    CONSTRAINT Category_pkey PRIMARY KEY (Id)
)
TABLESPACE pg_default;

ALTER TABLE public.Category
    OWNER to postgres;

CREATE INDEX fki_FK_Election1
    ON public.Category USING btree
    (ElectionId ASC NULLS LAST)
    TABLESPACE pg_default;
	
CREATE TABLE public.CategoryType
(
    Id INT GENERATED ALWAYS AS IDENTITY,
    Description text COLLATE pg_catalog.default NOT NULL,
    CreateDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastUpdated timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Active boolean NOT NULL DEFAULT true,
    CONSTRAINT CategoryType_pkey PRIMARY KEY (Id)
)
TABLESPACE pg_default;

ALTER TABLE public.CategoryType
    OWNER to postgres;
	
CREATE TABLE public.Election
(
    Id uuid NOT NULL,
    Date date,
    StartDateLocal timestamp without time zone,
    EndDateLocal timestamp without time zone,
    Description text COLLATE pg_catalog.default,
    CreateDate timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    LastUpdated timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    Version text COLLATE pg_catalog.default,
    AllowUpdates boolean DEFAULT false,
    CONSTRAINT Election_pkey PRIMARY KEY (Id)
)
TABLESPACE pg_default;

ALTER TABLE public.Election
    OWNER to postgres;

CREATE TABLE public.Party
(
    Id int GENERATED ALWAYS AS IDENTITY,
    Description text COLLATE pg_catalog.default NOT NULL,
    CreateDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastUpdated timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Active boolean NOT NULL DEFAULT true,
    CONSTRAINT Party_pkey PRIMARY KEY (Id)
)
TABLESPACE pg_default;

ALTER TABLE public.Party
    OWNER to postgres;

CREATE TABLE public.Signature
(
    Id uuid NOT NULL,
    BallotId uuid NOT NULL,
    ElectionId uuid NOT NULL,
    Name text COLLATE pg_catalog.default NOT NULL,
    Confirmed boolean NOT NULL DEFAULT false,
    BirthYear integer NOT NULL,
    ImageArray bytea NOT NULL,
    DeviceId text COLLATE pg_catalog.default,
    Longitude double precision,
    Latitude double precision,
    Platform integer NOT NULL,
    PreviousSignature uuid,
    CreateDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastUpdatedDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    SignatureStatus integer NOT NULL DEFAULT 0,
    SubmitDate timestamp without time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Signature_pkey PRIMARY KEY (Id)
)
TABLESPACE pg_default;

ALTER TABLE public.Signature
    OWNER to postgres;
	
CREATE TABLE public.SignatureNotice
(
    Id uuid NOT NULL,
    Nonce integer NOT NULL,
    BallotId uuid NOT NULL,
    CreateDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP
)
TABLESPACE pg_default;

ALTER TABLE public.SignatureNotice
    OWNER to postgres;

CREATE TABLE public.Ticket
(
    Id uuid NOT NULL,
    Description text, 
    PartyId integer NULL,
    CreateDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastUpdated timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
	Information text COLLATE pg_catalog.default,
    CategoryId uuid NOT NULL,
    Sequence integer NOT NULL DEFAULT 0,
    ElectionId uuid NOT NULL,
    TicketType integer NOT NULL DEFAULT 0,
    CONSTRAINT Ticket_pkey PRIMARY KEY (Id)
)
TABLESPACE pg_default;

ALTER TABLE public.Ticket
    OWNER to postgres;

CREATE TABLE public.Vote
(
    Id uuid NOT NULL,
    ElectionId uuid NOT NULL,
    BallotId uuid NOT NULL,
    CategoryId uuid NOT NULL,
    CategoryTypeId integer NOT NULL DEFAULT 0,
    SelectionId uuid NOT NULL,
    VoteStatus integer NOT NULL DEFAULT 0,
    CreateDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastUpdatedDate timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ApprovalDate timestamp with time zone NOT NULL,
    CONSTRAINT Vote_pkey PRIMARY KEY (Id)
)
TABLESPACE pg_default;

ALTER TABLE public.Vote
    OWNER to postgres;
	
	
CREATE TABLE public.VoteResult
(
    Id uuid NOT NULL,
    ElectionId uuid NOT NULL,
    CategoryId uuid,
    Count integer NOT NULL
)
TABLESPACE pg_default;

ALTER TABLE public.VoteResult
    OWNER to postgres;	

CREATE INDEX fki_FK_CategoryType1
    ON public.Category USING btree
    (CategoryTypeId ASC NULLS LAST)
    TABLESPACE pg_default;
	
ALTER TABLE public.Category	
    ADD CONSTRAINT FK_CategoryType1 FOREIGN KEY (CategoryTypeId)
        REFERENCES public.CategoryType (Id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    ADD CONSTRAINT FK_Election1 FOREIGN KEY (ElectionId)
        REFERENCES public.Election (Id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE;
		
ALTER TABLE public.Ticket	
    ADD CONSTRAINT FK_Category FOREIGN KEY (CategoryId)
        REFERENCES public.Category (Id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
	ADD CONSTRAINT FK_Party FOREIGN KEY (PartyId)
        REFERENCES public.Party (Id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    ADD CONSTRAINT FK_Election FOREIGN KEY (ElectionId)
        REFERENCES public.Election (Id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE;						
		

  		

GRANT ALL ON TABLE public.ballot TO "Robotuner";

GRANT ALL ON TABLE public.category TO "Robotuner";

GRANT ALL ON TABLE public.categorytype TO "Robotuner";

GRANT ALL ON TABLE public.election TO "Robotuner";

GRANT ALL ON TABLE public.party TO "Robotuner";

GRANT ALL ON TABLE public.signature TO "Robotuner";

GRANT ALL ON TABLE public.signaturenotice TO "Robotuner";

GRANT ALL ON TABLE public.ticket TO "Robotuner";

GRANT ALL ON TABLE public.vote TO "Robotuner";

GRANT ALL ON TABLE public.voteresult TO "Robotuner";
