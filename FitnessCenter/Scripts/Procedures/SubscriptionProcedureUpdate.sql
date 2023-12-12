create or replace procedure fc.subscription_procedure_update(
    p_id integer,
    p_visit integer
)
as
$$
begin
    update fc.subscription set visit = visit + p_visit where id = p_id;
end;
$$
language plpgsql;