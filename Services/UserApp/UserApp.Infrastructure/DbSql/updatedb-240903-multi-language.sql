CREATE SCHEMA IF NOT EXISTS usr_tr AUTHORIZATION admin;

CREATE TABLE tb_language (id varchar(36) NOT NULL, name varchar(50) NOT NULL, description varchar(255) NOT NULL, CONSTRAINT pk_ltb_language PRIMARY KEY (id));

CREATE TABLE usr_tr.appl (id varchar(36) NOT NULL, language_id varchar(36) NOT NULL, name varchar(255) NOT NULL, description varchar(500) NOT NULL, CONSTRAINT appl_pkey PRIMARY KEY (id, language_id));
CREATE TABLE usr_tr.appl_gallery (id varchar(36) NOT NULL, language_id varchar(36) NOT NULL, title varchar(50) NOT NULL, description varchar(500) NOT NULL, CONSTRAINT appl_gallery_pkey PRIMARY KEY (id, language_id));
CREATE TABLE usr_tr.appl_infographic (id varchar(36) NOT NULL, language_id varchar(36) NOT NULL, title varchar(255) NOT NULL, description varchar(500) NOT NULL, file_infographic varchar(255) NOT NULL, CONSTRAINT appl_infographic_pkey PRIMARY KEY (id, language_id));
CREATE TABLE usr_tr.appl_news (id varchar(36) NOT NULL, language_id varchar(36) NOT NULL, title varchar(500) NOT NULL, description text NOT NULL, header varchar(500) NOT NULL, CONSTRAINT appl_news_pkey PRIMARY KEY (id, language_id));
CREATE TABLE usr_tr.appl_news_category (id varchar(36) NOT NULL, language_id varchar(36) NOT NULL, name varchar(255) NOT NULL, description varchar(500) NOT NULL, CONSTRAINT appl_news_category_pkey PRIMARY KEY (id, language_id));
CREATE TABLE usr_tr.appl_task (id varchar(36) NOT NULL, language_id varchar(36) NOT NULL, task_name varchar(255) NOT NULL, description varchar(255) NOT NULL, CONSTRAINT appl_task_pkey PRIMARY KEY (id, language_id));
CREATE TABLE usr_tr.ref_tables (id varchar(36) NOT NULL, language_id varchar(36) NOT NULL, info jsonb NOT NULL, CONSTRAINT pk_ref_tables PRIMARY KEY (id, language_id));
ALTER TABLE usr_tr.appl_gallery ADD CONSTRAINT appl_gallery_tr_id_fkey FOREIGN KEY (id) REFERENCES usr.appl_gallery (id);
ALTER TABLE usr_tr.appl_infographic ADD CONSTRAINT appl_infographic_tr_id_fkey FOREIGN KEY (id) REFERENCES usr.appl_infographic (id);
ALTER TABLE usr_tr.appl_news_category ADD CONSTRAINT appl_news_category_tr_id_fkey FOREIGN KEY (id) REFERENCES usr.appl_news_category (id);
ALTER TABLE usr_tr.appl_news ADD CONSTRAINT appl_news_tr_id_fkey FOREIGN KEY (id) REFERENCES usr.appl_news (id);
ALTER TABLE usr_tr.appl_task ADD CONSTRAINT appl_task_tr_id_fkey FOREIGN KEY (id) REFERENCES usr.appl_task (id);
ALTER TABLE usr_tr.appl_task ADD CONSTRAINT appl_task_tr_language_id_fkey FOREIGN KEY (language_id) REFERENCES tb_language (id);
ALTER TABLE usr_tr.appl ADD CONSTRAINT appl_tr_id_fkey FOREIGN KEY (id) REFERENCES usr.appl (id);
ALTER TABLE usr_tr.appl ADD CONSTRAINT appl_tr_language_id_fkey FOREIGN KEY (language_id) REFERENCES tb_language (id);
ALTER TABLE usr_tr.appl_gallery ADD CONSTRAINT gallery_tr_language_id_fkey FOREIGN KEY (language_id) REFERENCES tb_language (id);
ALTER TABLE usr_tr.appl_infographic ADD CONSTRAINT infographic_tr_language_id_fkey FOREIGN KEY (language_id) REFERENCES tb_language (id);
ALTER TABLE usr_tr.appl_news_category ADD CONSTRAINT news_category_tr_language_id_fkey FOREIGN KEY (language_id) REFERENCES tb_language (id);
ALTER TABLE usr_tr.appl_news ADD CONSTRAINT news_tr_language_id_fkey FOREIGN KEY (language_id) REFERENCES tb_language (id);
ALTER TABLE usr_tr.ref_tables ADD CONSTRAINT ref_table_tr_language_id_fkey FOREIGN KEY (language_id) REFERENCES tb_language (id);
ALTER TABLE usr_tr.ref_tables ADD CONSTRAINT ref_tables_tr_id_fkey FOREIGN KEY (id) REFERENCES usr.ref_tables (id);

INSERT INTO public.tb_language (id, name, description) VALUES ('en', 'English', 'English');

INSERT INTO usr_tr.appl (id, language_id, name, description) VALUES ('adm', 'en', 'KOKKUN Data Management Application', 'KOKKUN Data Management Application');
INSERT INTO usr_tr.appl (id, language_id, name, description) VALUES ('web', 'en', 'KOKKUN Frontend (web) Application', 'KOKKUN Frontend (web) Application');

INSERT INTO usr_tr.appl_news_category (id, language_id, name, description) VALUES ('1', 'en', 'Straight News', 'Straight news is a report of events that is written briefly, concisely, straightforwardly and matter-of-factly.');
INSERT INTO usr_tr.appl_news_category (id, language_id, name, description) VALUES ('2', 'en', 'Opinion news', 'Opinion news is news about someone''s opinion, statement, or idea, usually the opinion of scholars, scholars, experts, or officials, about an event.');


INSERT INTO usr_tr.appl_task (id, language_id, task_name, description) VALUES ('adm-01', 'en', 'Utilities', 'Utilities Page');
INSERT INTO usr_tr.appl_task (id, language_id, task_name, description) VALUES ('adm-01.01', 'en', 'Users', 'Users Menu');

--insert history update
INSERT INTO public.database_change_log(
	filename, executed_date)
	VALUES ('updatedb-240903-multi-language.sql', now());