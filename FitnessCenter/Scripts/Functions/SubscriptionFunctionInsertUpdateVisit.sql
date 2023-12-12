create or replace function fc.subscription_function_insert_update_visit()
returns trigger as $$
begin
    if new.visit < 0 then
        new.visit := 0;
    end if;
    return new;
end;
$$
language plpgsql;