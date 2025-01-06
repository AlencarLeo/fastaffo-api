metodo construtores:
- nao pode retornar nada
- mesmo nome da classe
- executa toda vez que classe e criada



appsettigns.json
- geral
- default, mas sobreescrita por ambientes

appsettigns.dev.json
- ambiente dev

appsettigns.prd.json
- ambiente prd

appsettigns.test.json
- ambiente test



-> COMO DEFINIR O AMBIENTE Q ESTA SENDO USADO PARA USAR O APPSETTINGS???
-> RELACAO ENTRE VERSAO DO CODIGO, AMBIENTE E VARIAVEIS DE AMBIENTE


FEATURES
Adicionar email para criacao de conta, recuperacao e auth em geral...



SEGURANCA

Brute force e bots ->

Vunerabilidades:
    - REgistro de usuario revela q email esta em uso, SOLUCAO -> criar conta com o mesmo email vai "repetir" o processo de recuperacao,
        -> EMAIL: "Parece que voce tentou criar uma conta nova, mas voce ja tem clique aqui para recuperar a senha" -> recuperar senha exige 2 fatores com celular
    
    - Adicionar Captcha







# Tasks
- [x] fix UserStaffDtoReq & UserStaffDtoRes repetidas 
- [x] Criptografar senha UserAdmin
- [x] Mudar UserAdmin para controller de Auth?
- [x] DTO Res para UserAdmin no get
- [x] DTO Res para UserAdmin no get -> inserir Company

- [x] Get Companies
- [x] Post Companies need to be owner
- [x] Add Company member
- [x] Put Companies need to be owner

-------------------------------------------------------------

- [ ] FIX LOGIN
- [ ] AUTHORIZATION

- [ ] FIX AND REVIEW ---JOB--- (not JOBS) CONTROLLER
- [ ] Post Job need to be admin
- [ ] Put Job need to be admin
- [ ] Delete Job need to be admin

- [ ] job entity - assineged to -> accept, denied, ...

- [ ] Pagination and logic past/next jobs

- [ ] REMOVER STAFF DO jobs



- APOS CRIAR REQUEST E FOR ACEITO -> MUDAR LISTA DE IDS EM JOBS


------------------------------------------------------------
FEATURE META THIS WEEK:
- base LOGIN/REGISTER working completely (labour app and admin)
- base NEXT and PAST jobs working completely (labour app and admin)
- base CALENDAR jobs working completely (labour app and admin)
- base CONFLICTS jobs started


- ID JOBS TODOS 0000000000 (verificar outros)

- ADD A JOB TO MYSELF
- PUSH NOTIFICATION

- JOB VISIBLE FOR (NEW PROP IN JOBS???)


NEED TO BE SOMEONE
NEED VERB ING




- ELE CRIOU O JWT, NO VIDEO SO VAI USAR O GERADOR DE TOKEN DO DOTNET