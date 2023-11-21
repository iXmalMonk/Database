create user user1 with password 'password1';
create user user2 with password 'password2';
create user user3 with password 'password3';
create group group1;

grant usage on schema fc to user1;
grant select on fc.body_mass_index to user1;
grant select on fc.visit to user2;
grant usage on schema fc to group group1;
grant select on fc.client to group1;
grant insert, update, delete on fc.group to group1;
grant group1 to user2;
grant group1 to user3;
