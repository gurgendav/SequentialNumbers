# SequentialNumbers
Very simple .Net Web API project which on every request returns next number

## API


`GET /api/numbers?key=KEY` - Get next number in KEY sequence


`POST /api/numbers` - Create new sequence with start value
```
{
  "Key": "KEY",
  "StartValue": 8 // To create sequence with start value 8
}
```

## Currently available at

https://sequential-numbers.herokuapp.com/api/numbers?key=test
