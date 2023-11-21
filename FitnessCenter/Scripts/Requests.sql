--select weight from fc.body_mass_index where height >= 1.88;

--select distinct weight from fc.body_mass_index where height >= 1.88;

--select * from fc.client where full_name like 'Ильин Александр Георгиевич';
--select * from fc.body_mass_index where height in (1.6, 1.7);
--select * from fc.body_mass_index where height between 1.6 and 1.8;
--select * from fc.visit where client_id is null;
--select * from fc.visit where trainer_id is not null;
--select * from fc.body_mass_index order by height asc;
--select weight, count(height) from fc.body_mass_index group by weight;

--select count(height) from fc.body_mass_index;
--select sum(height) from fc.body_mass_index;

--select height, count(*) from fc.body_mass_index where weight > 70 group by height;

