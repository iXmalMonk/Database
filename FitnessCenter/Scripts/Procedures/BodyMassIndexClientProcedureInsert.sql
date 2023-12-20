create or replace procedure fc.body_mass_index_client_procedure_insert(
    p_full_name fc.fn,
    p_weight decimal,
    p_height decimal
)
as
$$
declare
	v_body_mass_index_id integer;
begin
    insert into fc.body_mass_index (weight, height)
    values (p_weight, p_height)
    returning id into v_body_mass_index_id;

    insert into fc.client (full_name, body_mass_index_id)
    values (p_full_name, v_body_mass_index_id);
end;
$$
language plpgsql;