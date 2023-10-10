create table fc.group(
    uuid uuid not null default gen_random_uuid(),
    id integer primary key generated by default as identity,
    title varchar(64) not null,
    timetable_of_classes_id integer not null unique,
    type_of_occupation_id integer not null,
    foreign key (timetable_of_classes_id) references fc.timetable_of_classes(id)
    on delete cascade,
    foreign key (type_of_occupation_id) references fc.type_of_occupation(id)
    on delete cascade
)
