create trigger body_mass_index_trigger_update_bms
before update of weight, height on fc.body_mass_index
for each row
execute function fc.body_mass_index_function_update_bms();