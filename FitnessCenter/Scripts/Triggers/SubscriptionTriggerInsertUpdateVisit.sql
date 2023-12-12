create trigger subscription_trigger_insert_update_visit
before insert or update on fc.subscription
for each row
execute function fc.subscription_function_insert_update_visit();