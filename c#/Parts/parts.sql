--------------------------------------------------------
--  File created - Thursday-February-06-2014   
--------------------------------------------------------
DROP TABLE "PARTS"."ALLOY";
DROP TABLE "PARTS"."DEP";
DROP TABLE "PARTS"."PART";
DROP TABLE "PARTS"."STAGE";
DROP TABLE "PARTS"."SURFACE";
DROP TABLE "PARTS"."TYPE_DEP";
DROP SEQUENCE "PARTS"."SEQ_ALLOY";
DROP SEQUENCE "PARTS"."SEQ_DEP";
DROP SEQUENCE "PARTS"."SEQ_PART";
DROP SEQUENCE "PARTS"."SEQ_STAGE";
DROP SEQUENCE "PARTS"."SEQ_SURFACE";
DROP SEQUENCE "PARTS"."SEQ_TYPE_DEP";
DROP PROCEDURE "PARTS"."ALLOY_CHANGE_ITEM";
DROP PROCEDURE "PARTS"."DEP_CHANGE_ITEM";
DROP PROCEDURE "PARTS"."PART_CHANGE_ITEM";
DROP PROCEDURE "PARTS"."STAGE_CHANGE_ITEM";
DROP PROCEDURE "PARTS"."SURFACE_CHANGE_ITEM";
DROP PROCEDURE "PARTS"."TYPE_DEP_CHANGE_ITEM";
--------------------------------------------------------
--  DDL for Sequence SEQ_ALLOY
--------------------------------------------------------

   CREATE SEQUENCE  "PARTS"."SEQ_ALLOY"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 44 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQ_DEP
--------------------------------------------------------

   CREATE SEQUENCE  "PARTS"."SEQ_DEP"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 49 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQ_PART
--------------------------------------------------------

   CREATE SEQUENCE  "PARTS"."SEQ_PART"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 27 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQ_STAGE
--------------------------------------------------------

   CREATE SEQUENCE  "PARTS"."SEQ_STAGE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 28 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQ_SURFACE
--------------------------------------------------------

   CREATE SEQUENCE  "PARTS"."SEQ_SURFACE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 27 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQ_TYPE_DEP
--------------------------------------------------------

   CREATE SEQUENCE  "PARTS"."SEQ_TYPE_DEP"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 28 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Table ALLOY
--------------------------------------------------------

  CREATE TABLE "PARTS"."ALLOY" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(256 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table DEP
--------------------------------------------------------

  CREATE TABLE "PARTS"."DEP" 
   (	"ID" NUMBER, 
	"ID_TYPE_DEP" NUMBER, 
	"NUM" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PART
--------------------------------------------------------

  CREATE TABLE "PARTS"."PART" 
   (	"ID" NUMBER, 
	"ID_ALLOY" NUMBER, 
	"NAME" VARCHAR2(256 BYTE), 
	"COST" NUMBER DEFAULT 0, 
	"BLNUM" VARCHAR2(20 CHAR)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table STAGE
--------------------------------------------------------

  CREATE TABLE "PARTS"."STAGE" 
   (	"ID" NUMBER, 
	"ID_PREV" NUMBER DEFAULT 0, 
	"ID_NEXT" NUMBER DEFAULT 0, 
	"ID_DEP" NUMBER, 
	"ID_PART" NUMBER, 
	"ID_SURFACE" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table SURFACE
--------------------------------------------------------

  CREATE TABLE "PARTS"."SURFACE" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(256 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table TYPE_DEP
--------------------------------------------------------

  CREATE TABLE "PARTS"."TYPE_DEP" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(256 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
REM INSERTING into PARTS.ALLOY
SET DEFINE OFF;
Insert into PARTS.ALLOY (ID,NAME) values (24,'Бронза');
Insert into PARTS.ALLOY (ID,NAME) values (27,'Высокоуглеродистая сталь');
Insert into PARTS.ALLOY (ID,NAME) values (28,'Дюралюминий');
Insert into PARTS.ALLOY (ID,NAME) values (29,'Латунь');
Insert into PARTS.ALLOY (ID,NAME) values (1,'Неопределен');
Insert into PARTS.ALLOY (ID,NAME) values (26,'Чугун');
REM INSERTING into PARTS.DEP
SET DEFINE OFF;
Insert into PARTS.DEP (ID,ID_TYPE_DEP,NUM) values (1,1,0);
Insert into PARTS.DEP (ID,ID_TYPE_DEP,NUM) values (29,9,19);
Insert into PARTS.DEP (ID,ID_TYPE_DEP,NUM) values (30,10,98);
Insert into PARTS.DEP (ID,ID_TYPE_DEP,NUM) values (31,8,29);
Insert into PARTS.DEP (ID,ID_TYPE_DEP,NUM) values (32,11,1);
Insert into PARTS.DEP (ID,ID_TYPE_DEP,NUM) values (33,12,21);
REM INSERTING into PARTS.PART
SET DEFINE OFF;
Insert into PARTS.PART (ID,ID_ALLOY,NAME,COST,BLNUM) values (9,27,'Втулка',2000,'234214');
Insert into PARTS.PART (ID,ID_ALLOY,NAME,COST,BLNUM) values (1,1,'Заготовка',0,'0');
Insert into PARTS.PART (ID,ID_ALLOY,NAME,COST,BLNUM) values (8,26,'Кольцо',50,'234905');
REM INSERTING into PARTS.STAGE
SET DEFINE OFF;
Insert into PARTS.STAGE (ID,ID_PREV,ID_NEXT,ID_DEP,ID_PART,ID_SURFACE) values (1,1,1,1,1,1);
Insert into PARTS.STAGE (ID,ID_PREV,ID_NEXT,ID_DEP,ID_PART,ID_SURFACE) values (10,1,1,29,9,4);
REM INSERTING into PARTS.SURFACE
SET DEFINE OFF;
Insert into PARTS.SURFACE (ID,NAME) values (7,'Борирование');
Insert into PARTS.SURFACE (ID,NAME) values (1,'Не требует обработки');
Insert into PARTS.SURFACE (ID,NAME) values (4,'Оксидирование');
Insert into PARTS.SURFACE (ID,NAME) values (3,'Покраска');
REM INSERTING into PARTS.TYPE_DEP
SET DEFINE OFF;
Insert into PARTS.TYPE_DEP (ID,NAME) values (12,'Гальванообработки');
Insert into PARTS.TYPE_DEP (ID,NAME) values (11,'Заготовочный');
Insert into PARTS.TYPE_DEP (ID,NAME) values (10,'Инструментальный');
Insert into PARTS.TYPE_DEP (ID,NAME) values (9,'Механический');
Insert into PARTS.TYPE_DEP (ID,NAME) values (1,'Начальный/конечный');
Insert into PARTS.TYPE_DEP (ID,NAME) values (8,'Сборочный');
--------------------------------------------------------
--  DDL for Index DEP_UK1
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."DEP_UK1" ON "PARTS"."DEP" ("NUM") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index TYPE_DEP_UK1
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."TYPE_DEP_UK1" ON "PARTS"."TYPE_DEP" ("NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index STAGE_UK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."STAGE_UK" ON "PARTS"."STAGE" ("ID_PREV", "ID_NEXT", "ID_DEP", "ID_PART", "ID_SURFACE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index PART_UK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."PART_UK" ON "PARTS"."PART" ("ID_ALLOY", "NAME", "BLNUM") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SURFACE_UK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."SURFACE_UK" ON "PARTS"."SURFACE" ("NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index TYPE_DEP_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."TYPE_DEP_PK" ON "PARTS"."TYPE_DEP" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index STAGE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."STAGE_PK" ON "PARTS"."STAGE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SURFACE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."SURFACE_PK" ON "PARTS"."SURFACE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index DEP_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."DEP_PK" ON "PARTS"."DEP" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index ALLOY_UK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."ALLOY_UK" ON "PARTS"."ALLOY" ("NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index ALLOY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."ALLOY_PK" ON "PARTS"."ALLOY" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index PART_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PARTS"."PART_PK" ON "PARTS"."PART" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table PART
--------------------------------------------------------

  ALTER TABLE "PARTS"."PART" ADD CONSTRAINT "PART_UK" UNIQUE ("ID_ALLOY", "NAME", "BLNUM")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."PART" MODIFY ("BLNUM" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."PART" ADD CONSTRAINT "PART_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."PART" MODIFY ("COST" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."PART" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."PART" MODIFY ("ID_ALLOY" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."PART" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table DEP
--------------------------------------------------------

  ALTER TABLE "PARTS"."DEP" ADD CONSTRAINT "DEP_UK1" UNIQUE ("NUM")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."DEP" MODIFY ("NUM" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."DEP" ADD CONSTRAINT "DEP_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."DEP" MODIFY ("ID_TYPE_DEP" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."DEP" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table SURFACE
--------------------------------------------------------

  ALTER TABLE "PARTS"."SURFACE" ADD CONSTRAINT "SURFACE_UK" UNIQUE ("NAME")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."SURFACE" ADD CONSTRAINT "SURFACE_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."SURFACE" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."SURFACE" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table ALLOY
--------------------------------------------------------

  ALTER TABLE "PARTS"."ALLOY" ADD CONSTRAINT "ALLOY_UK" UNIQUE ("NAME")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."ALLOY" ADD CONSTRAINT "ALLOY_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."ALLOY" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."ALLOY" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TYPE_DEP
--------------------------------------------------------

  ALTER TABLE "PARTS"."TYPE_DEP" ADD CONSTRAINT "TYPE_DEP_UK1" UNIQUE ("NAME")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."TYPE_DEP" ADD CONSTRAINT "TYPE_DEP_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."TYPE_DEP" MODIFY ("ID" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."TYPE_DEP" MODIFY ("NAME" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table STAGE
--------------------------------------------------------

  ALTER TABLE "PARTS"."STAGE" ADD CONSTRAINT "STAGE_UK" UNIQUE ("ID_PREV", "ID_NEXT", "ID_DEP", "ID_PART", "ID_SURFACE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."STAGE" ADD CONSTRAINT "STAGE_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "PARTS"."STAGE" MODIFY ("ID_SURFACE" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."STAGE" MODIFY ("ID_PART" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."STAGE" MODIFY ("ID_DEP" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."STAGE" MODIFY ("ID_NEXT" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."STAGE" MODIFY ("ID_PREV" NOT NULL ENABLE);
  ALTER TABLE "PARTS"."STAGE" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table DEP
--------------------------------------------------------

  ALTER TABLE "PARTS"."DEP" ADD CONSTRAINT "DEP_TYPE_DEP_FK1" FOREIGN KEY ("ID_TYPE_DEP")
	  REFERENCES "PARTS"."TYPE_DEP" ("ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PART
--------------------------------------------------------

  ALTER TABLE "PARTS"."PART" ADD CONSTRAINT "PART_ALLOY_FK1" FOREIGN KEY ("ID_ALLOY")
	  REFERENCES "PARTS"."ALLOY" ("ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table STAGE
--------------------------------------------------------

  ALTER TABLE "PARTS"."STAGE" ADD CONSTRAINT "STAGE_DEP_FK1" FOREIGN KEY ("ID_DEP")
	  REFERENCES "PARTS"."DEP" ("ID") ENABLE;
  ALTER TABLE "PARTS"."STAGE" ADD CONSTRAINT "STAGE_NEXT_FK1" FOREIGN KEY ("ID_NEXT")
	  REFERENCES "PARTS"."STAGE" ("ID") ENABLE;
  ALTER TABLE "PARTS"."STAGE" ADD CONSTRAINT "STAGE_PART_FK1" FOREIGN KEY ("ID_PART")
	  REFERENCES "PARTS"."PART" ("ID") ENABLE;
  ALTER TABLE "PARTS"."STAGE" ADD CONSTRAINT "STAGE_PREV_FK1" FOREIGN KEY ("ID_PREV")
	  REFERENCES "PARTS"."STAGE" ("ID") ENABLE;
  ALTER TABLE "PARTS"."STAGE" ADD CONSTRAINT "STAGE_SURFACE_FK1" FOREIGN KEY ("ID_SURFACE")
	  REFERENCES "PARTS"."SURFACE" ("ID") ENABLE;
--------------------------------------------------------
--  DDL for Trigger BI_ALLOY
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PARTS"."BI_ALLOY" 
BEFORE INSERT ON ALLOY 
FOR EACH ROW 
BEGIN
  if :new.id is null or :new.id < 1 then
    select seq_alloy.nextval into :new.id from dual;
  end if;
END;
/
ALTER TRIGGER "PARTS"."BI_ALLOY" ENABLE;
--------------------------------------------------------
--  DDL for Trigger BI_DEP
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PARTS"."BI_DEP" 
BEFORE INSERT ON DEP 
FOR EACH ROW 
BEGIN
  if :new.id is null or :new.id < 1 then
    select seq_dep.nextval into :new.id from dual;
  end if;
END;
/
ALTER TRIGGER "PARTS"."BI_DEP" ENABLE;
--------------------------------------------------------
--  DDL for Trigger BI_PART
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PARTS"."BI_PART" 
BEFORE INSERT ON PART 
FOR EACH ROW 
BEGIN
  if :new.id is null or :new.id < 1 then
    select seq_part.nextval into :new.id from dual;
  end if;
END;
/
ALTER TRIGGER "PARTS"."BI_PART" ENABLE;
--------------------------------------------------------
--  DDL for Trigger BI_STAGE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PARTS"."BI_STAGE" 
BEFORE INSERT ON STAGE
FOR EACH ROW 
BEGIN
  if :new.id is null or :new.id < 1 then
    select seq_stage.nextval into :new.id from dual;
  end if;
END;
/
ALTER TRIGGER "PARTS"."BI_STAGE" ENABLE;
--------------------------------------------------------
--  DDL for Trigger BI_SURFACE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PARTS"."BI_SURFACE" 
BEFORE INSERT ON SURFACE 
FOR EACH ROW 
BEGIN
  if :new.id is null or :new.id < 1 then
    select seq_surface.nextval into :new.id from dual;
  end if;
END;
/
ALTER TRIGGER "PARTS"."BI_SURFACE" ENABLE;
--------------------------------------------------------
--  DDL for Trigger BI_TYPE_DEP
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PARTS"."BI_TYPE_DEP" 
BEFORE INSERT ON TYPE_DEP
FOR EACH ROW 
BEGIN
  if :new.id is null or :new.id < 1 then
    select seq_type_dep.nextval into :new.id from dual;
  end if;
END;
/
ALTER TRIGGER "PARTS"."BI_TYPE_DEP" ENABLE;
--------------------------------------------------------
--  DDL for Procedure ALLOY_CHANGE_ITEM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "PARTS"."ALLOY_CHANGE_ITEM" 
(
  NEW_NAME IN VARCHAR2  
, OLD_ID IN NUMBER  
, NEW_ID OUT NUMBER  
) AS 
  TYPE GenericCursor IS REF CURSOR;
  findCur GenericCursor;
BEGIN
  if old_id is null or old_id = 0 then
    insert into alloy(name) values(new_name)
    returning id into new_id;
  else
    open findCur for select id from alloy where upper(name) = upper(new_name);
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := null;
    end if;
    close findCur;
    
    if new_id is null or new_id = old_id then
      update alloy set name = new_name where id = old_id;
      new_id := old_id;
    end if;
  end if;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN
    open findCur for select id from alloy where upper(name) = upper(new_name);
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := 1;
    end if;
    -- ghghghghgh
    /*
    */
    close findCur;
END ALLOY_CHANGE_ITEM;

/
--------------------------------------------------------
--  DDL for Procedure DEP_CHANGE_ITEM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "PARTS"."DEP_CHANGE_ITEM" 
(
  NEW_ID_TYPE_DEP IN NUMBER 
, NEW_NUM IN NUMBER
, OLD_ID IN NUMBER  
, NEW_ID OUT NUMBER  
) AS 
  TYPE GenericCursor IS REF CURSOR;
  findCur GenericCursor;
BEGIN
  if old_id is null or old_id = 0 then
    insert into dep(ID_TYPE_DEP, NUM) values(new_id_type_dep, new_num)
    returning id into new_id;
  else
    open findCur for select id from dep where num = new_num;
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := null;
    end if;
    close findCur;
    
    if new_id is null or new_id = old_id then
      update dep set id_type_dep = new_id_type_dep, num = new_num where id = old_id;
      new_id := old_id;
    end if;
  end if;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN
    open findCur for select id from dep where num = new_num;
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := 1;
    end if;
    close findCur;
END;

/
--------------------------------------------------------
--  DDL for Procedure PART_CHANGE_ITEM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "PARTS"."PART_CHANGE_ITEM" 
(
  NEW_ID_ALLOY IN NUMBER 
, NEW_BLNUM IN VARCHAR2
, NEW_NAME in varchar2
, new_cost in number
, OLD_ID IN NUMBER  
, NEW_ID OUT NUMBER  
) AS 
  TYPE GenericCursor IS REF CURSOR;
  findCur GenericCursor;
BEGIN
  if old_id is null or old_id = 0 then
    insert into part(ID_ALLOY, BLNUM, NAME, cost) values(NEW_ID_ALLOY, NEW_BLNUM, NEW_NAME, new_cost)
    returning id into new_id;
  else
    open findCur for select id from part where 
      ID_ALLOY = NEW_ID_ALLOY and
      BLNUM = NEW_BLNUM and
      NAME = NEW_NAME;
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := null;
    end if;
    close findCur;
    
    if new_id is null or new_id = old_id then
      update part set 
        ID_ALLOY = NEW_ID_ALLOY,
        BLNUM = NEW_BLNUM,
        NAME = NEW_NAME,
        cost = new_cost
      where id = old_id;
      new_id := old_id;
    end if;
  end if;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN
    open findCur for select id from part where 
      ID_ALLOY = NEW_ID_ALLOY and
      BLNUM = NEW_BLNUM and
      NAME = NEW_NAME;
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := 1;
    end if;
    close findCur;
END;

/
--------------------------------------------------------
--  DDL for Procedure STAGE_CHANGE_ITEM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "PARTS"."STAGE_CHANGE_ITEM" 
(
  NEW_id_prev IN NUMBER 
, NEW_id_next IN NUMBER 
, NEW_id_dep IN NUMBER 
, NEW_id_part IN NUMBER 
, NEW_id_surface IN NUMBER 
, OLD_ID IN NUMBER  
, NEW_ID OUT NUMBER  
) AS 
  TYPE GenericCursor IS REF CURSOR;
  findCur GenericCursor;
BEGIN
  if old_id is null or old_id = 0 then
    insert into stage(
      id_prev
      , id_next
      , id_dep
      , id_part
      , id_surface) 
    values(NEW_id_prev
      , NEW_id_next
      , NEW_id_dep
      , NEW_id_part
      , NEW_id_surface)
    returning id into new_id;
  else
    open findCur for select id from stage where 
      id_prev = NEW_id_prev
      and id_next = NEW_id_next
      and id_dep = NEW_id_dep
      and id_part = NEW_id_part
      and id_surface = NEW_id_surface;
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := null;
    end if;
    close findCur;
    
    if new_id is null or new_id = old_id then
      update stage set 
        id_prev = NEW_id_prev
        , id_next = NEW_id_next
        , id_dep = NEW_id_dep
        , id_part = NEW_id_part
        , id_surface = NEW_id_surface
      where id = old_id;
      new_id := old_id;
    end if;
  end if;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN
    open findCur for select id from stage where 
      id_prev = NEW_id_prev
      and id_next = NEW_id_next
      and id_dep = NEW_id_dep
      and id_part = NEW_id_part
      and id_surface = NEW_id_surface;
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := 1;
    end if;
    close findCur;
END;

/
--------------------------------------------------------
--  DDL for Procedure SURFACE_CHANGE_ITEM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "PARTS"."SURFACE_CHANGE_ITEM" 
(
  NEW_NAME IN VARCHAR2  
, OLD_ID IN NUMBER  
, NEW_ID OUT NUMBER  
) AS 
  TYPE GenericCursor IS REF CURSOR;
  findCur GenericCursor;
BEGIN
  if old_id is null or old_id = 0 then
    insert into surface(name) values(new_name)
    returning id into new_id;
  else
    open findCur for select id from surface where upper(name) = upper(new_name);
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := null;
    end if;
    close findCur;
    
    if new_id is null or new_id = old_id then
      update surface set name = new_name where id = old_id;
      new_id := old_id;
    end if;
  end if;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN
    open findCur for select id from surface where upper(name) = upper(new_name);
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := 1;
    end if;
    -- ghghghghgh
    /*
    */
    close findCur;
END SURFACE_CHANGE_ITEM;

/
--------------------------------------------------------
--  DDL for Procedure TYPE_DEP_CHANGE_ITEM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "PARTS"."TYPE_DEP_CHANGE_ITEM" 
(
  NEW_NAME IN VARCHAR2  
, OLD_ID IN NUMBER  
, NEW_ID OUT NUMBER  
) AS 
  TYPE GenericCursor IS REF CURSOR;
  findCur GenericCursor;
BEGIN
  if old_id is null or old_id = 0 then
    insert into type_dep(name) values(new_name)
    returning id into new_id;
  else
    open findCur for select id from type_dep where upper(name) = upper(new_name);
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := null;
    end if;
    close findCur;
    
    if new_id is null or new_id = old_id then
      update type_dep set name = new_name where id = old_id;
      new_id := old_id;
    end if;
  end if;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN
    open findCur for select id from type_dep where upper(name) = upper(new_name);
    fetch findCur into new_id;
    if findCur%NOTFOUND then
      new_id := 1;
    end if;
    close findCur;
END;

/
