create table fc.day_of_the_week(
    uuid uuid not null default gen_random_uuid(),
    id integer primary key generated by default as identity,
    title varchar(64) not null
)