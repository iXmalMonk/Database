create or replace function fc.body_mass_index_function_update_bms()
returns trigger as $$
begin
    new.bms = new.weight / (new.height * new.height);
    return new;
end;
$$
language plpgsql;