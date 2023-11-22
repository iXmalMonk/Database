--select weight from fc.body_mass_index where height >= 1.88;

--select distinct weight from fc.body_mass_index where height >= 1.88;

--select * from fc.client where full_name like 'Ильин Александр Георгиевич';
--select * from fc.body_mass_index where height in (1.5, 1.9);
--select * from fc.body_mass_index where height between 1.5 and 1.75;
--select * from fc.visit where client_id is null;
--select * from fc.visit where trainer_id is not null;
--select * from fc.body_mass_index order by height asc;
--select weight, count(height) from fc.body_mass_index group by weight;

--select count(height) from fc.body_mass_index;
--select sum(height) from fc.body_mass_index;

--select height, count(*) from fc.body_mass_index where weight > 50 group by height;

--select * from fc.body_mass_index bmi inner join fc.client c on bmi.id = c.body_mass_index_id;

--select * from fc.body_mass_index bmi left join fc.client c on bmi.id = c.body_mass_index_id where c.body_mass_index_id >= 15;

--select c.full_name, bmi.weight, bmi.height from fc.client c inner join fc.body_mass_index bmi on c.body_mass_index_id = bmi.id where bmi.bms > 25;

--select * from fc.client where body_mass_index_id = any (select id from fc.body_mass_index where weight > 80);
--select * from fc.client where body_mass_index_id = all (select id from fc.body_mass_index where weight > 80);
--select * from fc.client c where exists (select id from fc.body_mass_index bmi where bmi.id = c.body_mass_index_id and bmi.bms > 25);

--select c.full_name from fc.client c where c.id = (select bmi.id from fc.body_mass_index bmi where bmi.id = c.id and bmi.bms between 18 and 25);

--insert into fc.body_mass_index (weight, height) values (80.8, 1.75);
--update fc.body_mass_index set weight = 80 where id = 1;
--delete from fc.body_mass_index where id = 1;