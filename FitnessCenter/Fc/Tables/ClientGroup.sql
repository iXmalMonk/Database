create table fc.client_group(
    uuid uuid not null default gen_random_uuid(),
    client_id integer not null,
    group_id integer not null,
    foreign key(client_id) references fc.client(id)
    on delete cascade,
    foreign key(group_id) references fc.group(id)
    on delete cascade
)
