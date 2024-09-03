CREATE TABLE "public".database_change_log (
    id serial NOT NULL, 
    filename varchar(255) NOT NULL, 
    executed_date timestamp(6) NOT NULL,
    CONSTRAINT database_change_log_pkey PRIMARY KEY (id),
    CONSTRAINT database_change_log_ukey UNIQUE (filename)
);
CREATE SCHEMA IF NOT EXISTS usr AUTHORIZATION admin;

CREATE TABLE usr.appl (
    id varchar(36) NOT NULL,
    code varchar(16) NOT NULL,
    name varchar(255) NOT NULL,
    description varchar(500) NOT NULL,
    bgcolor varchar(255) NOT NULL,
    iconfile varchar(255) NOT NULL,
    imagefile varchar(255) NOT NULL,
    CONSTRAINT appl_pkey PRIMARY KEY (id),
    CONSTRAINT appl_code_key UNIQUE (code)
);

CREATE TABLE usr.appl_extra (
    appl_id varchar(36) NOT NULL,
    "key" varchar(36) NOT NULL,
    TYPE varchar(36) NOT NULL,
    value varchar(255) NOT NULL,
    CONSTRAINT appl_extra_pkey PRIMARY KEY (appl_id, "key"),
    CONSTRAINT appl_extra_type_ckey CHECK (TYPE IN ('string', 'number', 'datetime', 'file'))
);

CREATE TABLE usr.appl_gallery (
    id varchar(36) NOT NULL,
    title varchar(50) NOT NULL,
    description varchar(500) NOT NULL,
    TYPE varchar(16) NOT NULL,
    file_thumbnail varchar(255) NOT NULL,
    file_gallery varchar(255) NOT NULL,
    is_banner bool NOT NULL,
    is_slider bool NOT NULL,
    is_approve bool NOT NULL,
    created_date timestamp NOT NULL,
    CONSTRAINT appl_gallery_pkey PRIMARY KEY (id),
    CONSTRAINT gallery_type_ckey CHECK (
        TYPE IN ('image', 'video', 'audio', 'application')
    )
);

CREATE TABLE usr.appl_infographic (
    id varchar(36) NOT NULL,
    title varchar(255) NOT NULL,
    description varchar(500) NOT NULL,
    file_thumbnail varchar(255) NOT NULL,
    file_infographic varchar(255) NOT NULL,
    is_approve bool NOT NULL,
    created_date timestamp NOT NULL,
    CONSTRAINT appl_infographic_pkey PRIMARY KEY (id)
);

CREATE TABLE usr.appl_news (
    id varchar(36) NOT NULL,
    appl_news_category_id varchar(36) NOT NULL,
    title varchar(500) NOT NULL,
    description text NOT NULL,
    TYPE varchar(16) NOT NULL,
    header varchar(500) NOT NULL,
    file_thumbnail varchar(255) NOT NULL,
    file_news varchar(255) NOT NULL,
    is_approve bool NOT NULL,
    created_date timestamp NOT NULL,
    CONSTRAINT appl_news_pkey PRIMARY KEY (id),
    CONSTRAINT news_type_ckey CHECK (TYPE IN ('image', 'video', 'audio'))
);

CREATE TABLE usr.appl_news_category (
    id varchar(36) NOT NULL,
    name varchar(255) NOT NULL,
    description varchar(500) NOT NULL,
    file_logo varchar(255) NOT NULL,
    CONSTRAINT appl_news_category_pkey PRIMARY KEY (id)
);

CREATE TABLE usr.appl_task (
    id varchar(36) NOT NULL,
    appl_task_parent_id varchar(36),
    appl_id varchar(36) NOT NULL,
    index_no int4 DEFAULT 0 NOT NULL,
    task_name varchar(255) NOT NULL,
    controller_name varchar(255),
    action_name varchar(255),
    description varchar(255) NOT NULL,
    icon_name varchar(255),
    custom_id varchar(36),
    STATUS int4 DEFAULT 1 NOT NULL,
    CONSTRAINT appl_task_pkey PRIMARY KEY (id)
);

CREATE TABLE usr.appl_task_delegation (
    id varchar(36) NOT NULL,
    appl_task_id varchar(36) NOT NULL,
    delegate_for varchar(36) NOT NULL,
    delegate_by varchar(36) NOT NULL,
    approved_by varchar(36),
    start_date date NOT NULL,
    end_date date NOT NULL,
    STATUS int4 NOT NULL,
    created_by varchar(36) NOT NULL,
    created_date timestamp(6) NOT NULL,
    updated_by varchar(36),
    updated_date timestamp(6),
    CONSTRAINT appl_task_delegation_pkey PRIMARY KEY (id)
);

CREATE TABLE usr.ref_tables (
    id varchar(36) NOT NULL,
    info jsonb NOT NULL,
    name varchar(255) NOT NULL,
    CONSTRAINT pk_ref_tables PRIMARY KEY (id)
);

CREATE TABLE usr.role (
    id varchar(36) NOT NULL,
    code varchar(16) NOT NULL,
    name varchar(64) NOT NULL,
    CONSTRAINT role_pkey PRIMARY KEY (id),
    CONSTRAINT role_code_key UNIQUE (code)
);

CREATE TABLE usr.role_appl_task (
    role_id varchar(36) NOT NULL,
    appl_task_id varchar(36) NOT NULL,
    CONSTRAINT role_appl_task_pkey PRIMARY KEY (role_id, appl_task_id)
);

CREATE TABLE usr."user" (
    id varchar(36) NOT NULL,
    username varchar(32) NOT NULL,
    PASSWORD varchar(128) NOT NULL,
    email varchar(64) NOT NULL,
    first_name varchar(30) NOT NULL,
    middle_name varchar(30),
    last_name varchar(30),
    address text NOT NULL,
    phone_number varchar(30),
    mobile_number varchar(30),
    STATUS int4 NOT NULL,
    last_login timestamp,
    created_by varchar(36) NOT NULL,
    created_date timestamp NOT NULL,
    updated_by varchar(36),
    updated_date timestamp,
    org_id varchar(36),
    CONSTRAINT users_pkey PRIMARY KEY (id),
    CONSTRAINT user_username_key UNIQUE (username),
    CONSTRAINT user_email_key UNIQUE (email),
    CONSTRAINT user_status_ckey CHECK (STATUS IN (0, 1, -1))
);

CREATE TABLE usr.user_file (
    user_id varchar(36) NOT NULL,
    TYPE varchar(16) NOT NULL,
    category varchar(16),
    file_name varchar(255) NOT NULL,
    file_thumbnail varchar(255),
    title varchar(255) NOT NULL,
    description text,
    CONSTRAINT pk_user_file PRIMARY KEY (user_id, TYPE),
    CONSTRAINT user_file_category_constant CHECK (category IN ('profile-picture')),
    CONSTRAINT user_file_type_ckey CHECK (
        TYPE IN (
            'image',
            'audio',
            'video',
            'pdf',
            'word',
            'sheet'
        )
    )
);

CREATE TABLE usr.user_role (
    user_id varchar(36) NOT NULL,
    role_id varchar(36) NOT NULL,
    CONSTRAINT users_roles_pkey PRIMARY KEY (user_id, role_id)
);

CREATE TABLE tb_history (
    id SERIAL NOT NULL,
    table_name varchar(255) NOT NULL,
    endpoint_name varchar(255) NOT NULL,
    created_by varchar(36) NOT NULL,
    created_date timestamp NOT NULL,
    note text,
    attach_file text,
    history_data jsonb NOT NULL,
    CONSTRAINT pk_history PRIMARY KEY (id)
);

ALTER TABLE
    usr.appl_extra
ADD
    CONSTRAINT appl_extra_appl_id_fkey FOREIGN KEY (appl_id) REFERENCES usr.appl (id);

ALTER TABLE
    usr.appl_news
ADD
    CONSTRAINT appl_news_news_category_id_fkey FOREIGN KEY (appl_news_category_id) REFERENCES usr.appl_news_category (id);

ALTER TABLE
    usr.appl_task
ADD
    CONSTRAINT appl_task_appl_id_fkey FOREIGN KEY (appl_id) REFERENCES usr.appl (id) ON UPDATE CASCADE ON DELETE No ACTION;

ALTER TABLE
    usr.appl_task_delegation
ADD
    CONSTRAINT appl_task_delegation_appl_task_id_fkey FOREIGN KEY (appl_task_id) REFERENCES usr.appl_task (id);

ALTER TABLE
    usr.appl_task_delegation
ADD
    CONSTRAINT appl_task_delegation_approve_by_fkey FOREIGN KEY (approved_by) REFERENCES usr."user" (id) ON UPDATE No ACTION ON DELETE No ACTION;

ALTER TABLE
    usr.appl_task_delegation
ADD
    CONSTRAINT appl_task_delegation_created_by_fkey FOREIGN KEY (created_by) REFERENCES usr."user" (id);

ALTER TABLE
    usr.appl_task_delegation
ADD
    CONSTRAINT appl_task_delegation_delegate_by_fkey FOREIGN KEY (delegate_by) REFERENCES usr."user" (id);

ALTER TABLE
    usr.appl_task_delegation
ADD
    CONSTRAINT appl_task_delegation_delegate_for_fkey FOREIGN KEY (delegate_for) REFERENCES usr."user" (id);

ALTER TABLE
    usr.appl_task_delegation
ADD
    CONSTRAINT appl_task_delegation_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES usr."user" (id);

ALTER TABLE
    usr.appl_task
ADD
    CONSTRAINT appl_task_parent_id_fkey FOREIGN KEY (appl_task_parent_id) REFERENCES usr.appl_task (id) ON UPDATE No ACTION ON DELETE No ACTION;

ALTER TABLE
    usr.user_file
ADD
    CONSTRAINT fk_user_file FOREIGN KEY (user_id) REFERENCES usr."user" (id) ON UPDATE No ACTION ON DELETE No ACTION;

ALTER TABLE
    usr.role_appl_task
ADD
    CONSTRAINT role_appl_task_appl_taks_id_fkey FOREIGN KEY (appl_task_id) REFERENCES usr.appl_task (id) ON UPDATE No ACTION ON DELETE No ACTION;

ALTER TABLE
    usr.role_appl_task
ADD
    CONSTRAINT role_appl_task_role_id_fkey FOREIGN KEY (role_id) REFERENCES usr.role (id);

ALTER TABLE
    usr."user"
ADD
    CONSTRAINT user_created_by_fkey FOREIGN KEY (created_by) REFERENCES usr."user" (id);

ALTER TABLE
    usr.user_role
ADD
    CONSTRAINT user_role_role_id_fkey FOREIGN KEY (role_id) REFERENCES usr.role (id);

ALTER TABLE
    usr.user_role
ADD
    CONSTRAINT user_role_user_id_fkey FOREIGN KEY (user_id) REFERENCES usr."user" (id);

ALTER TABLE
    usr."user"
ADD
    CONSTRAINT user_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES usr."user" (id);

ALTER TABLE
    tb_history
ADD
    CONSTRAINT tb_history_created_by_fkey FOREIGN KEY (created_by) REFERENCES usr."user" (id);

INSERT INTO
    usr.appl (
        id,
        code,
        name,
        description,
        bgcolor,
        iconfile,
        imagefile
    )
VALUES
    (
        'adm',
        '1',
        'Aplikasi Tatakelola Data KOKKUN',
        'Aplikasi Tatakelola Data KOKKUN',
        '#17a2b8',
        'iconfile.png',
        'imagefile.jpg'
    );

INSERT INTO
    usr.appl (
        id,
        code,
        name,
        description,
        bgcolor,
        iconfile,
        imagefile
    )
VALUES
    (
        'web',
        '2',
        'Aplikasi Frontend (web) KOKKUN',
        'Aplikasi Frontend (web) KOKKUN',
        '#17a2b8',
        'iconfile.png',
        'imagefile.jpg'
    );

INSERT INTO
    usr.appl_news_category (id, name, description, file_logo)
VALUES
    (
        '1',
        'Berita Langsung',
        'Berita langsung (straight news) adalah laporan peristiwa yang ditulis secara singkat, padat, lugas, dan apa adanya.',
        'no_logo.png'
    );

INSERT INTO
    usr.appl_news_category (id, name, description, file_logo)
VALUES
    (
        '2',
        'Berita Opini',
        'Berita opini (opinion news) yaitu berita mengenai pendapat, pernyataan, atau gagasan seseorang, biasanya pendapat para cendekiawan, sarjana, ahli, atau pejabat, mengenai suatu peristiwa.',
        'no_logo.png'
    );

INSERT INTO
    usr.appl_task (
        id,
        appl_task_parent_id,
        appl_id,
        index_no,
        task_name,
        controller_name,
        action_name,
        description,
        icon_name,
        custom_id,
        STATUS
    )
VALUES
    (
        'adm-01',
        NULL,
        'adm',
        10000,
        'Tata Kelola',
        'controller_name',
        'action_name',
        'Menu Tata Kelola Data Referensi dan Data Master',
        'icon_name',
        NULL,
        1
    );

INSERT INTO
    usr.appl_task (
        id,
        appl_task_parent_id,
        appl_id,
        index_no,
        task_name,
        controller_name,
        action_name,
        description,
        icon_name,
        custom_id,
        STATUS
    )
VALUES
    (
        'adm-01.01',
        'adm-01',
        'adm',
        10100,
        'Pengguna',
        'controller_name',
        'action_name',
        'Menu Tata Kelola Data Pengguna',
        'icon_name',
        NULL,
        1
    );

INSERT INTO
    usr.role (id, code, name)
VALUES
    ('public', 'public', 'Public Account');

INSERT INTO
    usr.role (id, code, name)
VALUES
    ('admin', 'admin', 'Administrator');

INSERT INTO
    usr.role (id, code, name)
VALUES
    ('staf', 'staf', 'Staf');

INSERT INTO
    usr.role_appl_task (role_id, appl_task_id)
VALUES
    ('admin', 'adm-01');

INSERT INTO
    usr.role_appl_task (role_id, appl_task_id)
VALUES
    ('admin', 'adm-01.01');

--AdminApp@2024
--Onlypublic
INSERT INTO
    usr.user (
        id,
        username,
        PASSWORD,
        email,
        first_name,
        middle_name,
        last_name,
        address,
        phone_number,
        mobile_number,
        org_id,
        STATUS,
        last_login,
        created_by,
        created_date,
        updated_by,
        updated_date
    )
VALUES
    (
        '01J4CBVMDZ5B3C65JSYWJJ4KGP',
        'admin',
        'J1RP0Ts9vzXRhGAhPltvzWz36OYrGaFJzP9HWnWXByk=',
        'admin@kokkun.id',
        'Administrator',
        'Sistem',
        'Aplikasi',
        'Kota Bogor',
        '08787006xxxxx',
        '08787006xxxxx',
        NULL,
        1,
        NULL,
        '01J4CBVMDZ5B3C65JSYWJJ4KGP',
        '2024-05-27 10:06:55.807876',
        NULL,
        NULL
    ),
    (
        'public',
        'public',
        'I8PND9lZnkQFo1finJmIUb9hCL1ClNiJBD4QeswhXNo=',
        'no_email@kokkun.id',
        'Pengguna',
        'public',
        NULL,
        'Kota Bogor',
        '08787006xxxxx',
        '08787006xxxxx',
        NULL,
        1,
        NULL,
        '01J4CBVMDZ5B3C65JSYWJJ4KGP',
        '2024-05-27 10:06:55.807876',
        NULL,
        NULL
    );

INSERT INTO
    usr.user_file(
        user_id,
        TYPE,
        category,
        file_name,
        file_thumbnail,
        title,
        description
    )
VALUES
    (
        '01J4CBVMDZ5B3C65JSYWJJ4KGP',
        'image',
        'profile-picture',
        'no-profile-picture.png',
        'no-profile-picture.png',
        'Pas Foto',
        'Pas Foto'
    );

INSERT INTO
    usr.user_role (user_id, role_id)
VALUES
    ('01J4CBVMDZ5B3C65JSYWJJ4KGP', 'admin');

--insert history update
INSERT INTO public.database_change_log(
	filename, executed_date)
	VALUES ('initDb.sql', now());    