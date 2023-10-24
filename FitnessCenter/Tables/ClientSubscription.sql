create table fc.client_subscription(
    uuid uuid not null default gen_random_uuid(),
    client_id integer not null,
    subscription_id integer not null,
    foreign key(client_id) references fc.client(id)
    on delete cascade,
    foreign key(subscription_id) references fc.subscription(id)
    on delete cascade
)
