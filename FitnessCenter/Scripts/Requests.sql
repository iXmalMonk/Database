--select * from fc.body_mass_index where height >= 1.88;

--select distinct on (fc.body_mass_index.height) * from fc.body_mass_index;

--select * from fc.client where full_name like '%а%';
--select * from fc.body_mass_index where height in (1.5, 1.9);
--select * from fc.body_mass_index where height between 1.5 and 1.75;
--select * from fc.visit where client_id is null;
--select * from fc.body_mass_index order by height asc;

--select count(height) from fc.body_mass_index;
--select sum(height) from fc.body_mass_index;

--select height, count(*) from fc.body_mass_index where weight > 50 group by height;

--select * from fc.body_mass_index bmi inner join fc.client c on bmi.id = c.body_mass_index_id;

--select * from fc.body_mass_index bmi left join fc.client c on bmi.id = c.body_mass_index_id;

--select * from fc.body_mass_index where bms > 25 or weight > 100;

--select * from fc.client where body_mass_index_id = any (select id from fc.body_mass_index where weight > 80);
--select * from fc.client where body_mass_index_id = all (select id from fc.body_mass_index where weight > 80);
--select * from fc.client c where exists (select * from fc.body_mass_index bmi where bmi.bms > 35 ); 

--insert into fc.body_mass_index (weight, height) values (80.8, 1.75);
--update fc.body_mass_index set weight = 80 where id = 1;
--delete from fc.body_mass_index where id = 1;