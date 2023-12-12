create or replace procedure fc.client_group_subscription_procedure_insert(
    p_client_id integer,
    p_group_id integer,
    p_visit integer
)
as
$$
declare
	v_type_of_occupation_id integer;
    v_subscription_id integer;
begin
    insert into fc.client_group (client_id, group_id)
    values (p_client_id, p_group_id);

    select type_of_occupation_id into v_type_of_occupation_id from fc.group where id = p_group_id;

    insert into fc.subscription (visit, type_of_occupation_id)
    values (p_visit, v_type_of_occupation_id)
    returning id into v_subscription_id;

    insert into fc.client_subscription(client_id, subscription_id)
    values (p_client_id, v_subscription_id);
end;
$$
language plpgsql;