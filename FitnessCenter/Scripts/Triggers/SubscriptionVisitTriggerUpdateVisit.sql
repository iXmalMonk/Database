create trigger subscription_visit_trigger_update_visit
after insert on fc.visit
for each row
execute function fc.subscription_visit_function_update_visit();