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

