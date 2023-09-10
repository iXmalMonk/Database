truncate table fc.group restart identity cascade;
truncate table fc.timetable_of_classes restart identity cascade;
truncate table fc.client restart identity cascade;
truncate table fc.trainer restart identity cascade;

drop table if exists fc.group cascade;
drop table if exists fc.timetable_of_classes cascade;
drop table if exists fc.client cascade;
drop table if exists fc.trainer cascade;
