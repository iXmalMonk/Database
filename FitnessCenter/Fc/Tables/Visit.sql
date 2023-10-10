create table fc.visit(
    uuid uuid not null default gen_random_uuid(),
    id integer primary key generated by default as identity,
    date_arrival varchar(32) not null,
    date_of_departure varchar(32) not null,
    client_id integer,
    trainer_id integer,
    foreign key (client_id) references fc.client(id)
    on delete cascade,
    foreign key (trainer_id) references fc.trainer(id)
    on delete cascade
)
