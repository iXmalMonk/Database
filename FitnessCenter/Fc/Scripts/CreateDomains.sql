create domain fc.fn as varchar(64) check(
    value ~ '^[А-Я][а-я]+ [А-Я][а-я]+ [А-Я][а-я]+$'
)