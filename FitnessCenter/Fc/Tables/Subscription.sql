create table fc.subscription(
    id integer primary key generated by default as identity,
    visit integer not null,
    type_of_occupation_id integer not null,
    foreign key(type_of_occupation_id) references fc.type_of_occupation(id)
    on delete cascade
)