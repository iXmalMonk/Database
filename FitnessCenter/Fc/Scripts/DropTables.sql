truncate table fc.client restart identity cascade;
truncate table fc.client_group restart identity cascade;
truncate table fc.day_of_the_week restart identity cascade;
truncate table fc.group restart identity cascade;
truncate table fc.time restart identity cascade;
truncate table fc.timetable_of_classes restart identity cascade;
truncate table fc.trainer restart identity cascade;

drop table if exists fc.client cascade;
drop table if exists fc.client_group cascade;
drop table if exists fc.day_of_the_week cascade;
drop table if exists fc.group cascade;
drop table if exists fc.time cascade;
drop table if exists fc.timetable_of_classes cascade;
drop table if exists fc.trainer cascade;
