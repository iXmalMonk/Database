create table fc.client_subscription(
    client_id integer not null,
    subscription_id integer not null,
    foreign key(client_id) references fc.client(id)
    on delete cascade,
    foreign key(subscription_id) references fc.subscription(id)
    on delete cascade
)
