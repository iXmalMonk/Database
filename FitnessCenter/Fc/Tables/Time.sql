create table fc.time(
    id integer primary key generated by default as identity,
    start_time text not null,
    end_time text not null
)
