create or replace function fc.subscription_visit_function_update_visit()
returns trigger as $$
begin
    if not exists (
        select 1 from fc.trainer where id = NEW.trainer_id
    ) then
        update fc.subscription
        set visit = visit - 1
        where id in (
            select subscription_id from fc.client_subscription
            where client_id = new.client_id
        );
    end if;
    return new;
end;
$$
language plpgsql;