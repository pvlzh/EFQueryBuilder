# EFQueryBuilder

Данная библиотека служит прослойкой для удобного/читабельного взаимодействия слоя бизнес правил с контекстом базы данных.

## Как это работает:
Взаимодействие с контекстом основано на введении абстракций `спецификации` и `проекции` для фильтрации и маппинга сущностей соответственно.

### Пример спецификации
Пользовательская спецификация реализует абстрактный тип библиотеки `Specification<TEntity>` где `TEntity` - тип сущности (таблица), по которой будет произведена фильтрация.
```csharp
public class ContractByName : Specification<Contract>
{
    protected override Expression<Func<Contract, bool>> Criteria { get; }
    
    public ContractByName(string contractName)
    {
        Criteria = contract => contract.Name == contractName;
    }
}
```

### Пример проекции
Пользовательская проекция также реализует абстрактный тип библиотеки `Projection<TEntity, TObject>` где `TEntity` - тип сущности, проекция которого будет описана в результирующий тип `TObject`.
```csharp
class ContractToContractResponse : Projection<Contract, ContractResponse>
{
    protected override Expression<Func<Contract, ContractResponse>> ProjectionExpression { get; }

    public ContractToContractResponse()
    {
        ProjectionExpression = contract =>
            new ContractResponse
            {
                Id = contract.Id,
                Description = contract.Description,
                Name = contract.Name,
                CreationDate = contract.CreationDate,
                ParticipantFio = contract.Participant.Fio
            };
    }
}
```
## Как с этим работать:
В классе контекста или репозитория (в данном руководстве все будет реализовано в контексте) необходимо реализовать методы для работы с абстракциями спецификации и проекции.

### Опишем интерфейс который будет использован для внедрения зависимостей.
```csharp
public interface IDataContext
{
    public IQuery<TEntity> Get<TEntity>(Specification<TEntity> specification)
        where TEntity : class;
    
    public IProjectionQuery<TObject> Get<TEntity, TObject>(Projection<TEntity, TObject> projection);
    
    // other methods (e.g. Add, Remove)
}
```
### Реализуем `IDataContext` 

*!Внимание: объявления `DbSet`, конструкторов и настройка в `OnModelCreating` для `DataContext` в данном примере пропущены. Пример кода целиком можно найти в папке `Example` исходного кода).*

В реализациях методов для удобства используем статичные методы библиотеки `QueryEvaluator.Evaluate` или при желании можно создать свои реализации `IQuery<TEntity>` и `IProjectionQuery<TObject>` и методы, которые их возвращают.
```csharp
internal class DataContext : DbContext, IDataContext
{
    public IQuery<TEntity> Get<TEntity>(Specification<TEntity> specification) 
        where TEntity : Entity
    {
        return QueryEvaluator.Evaluate(Set<TEntity>(), specification);
    }


    public IProjectionQuery<TObject> Get<TEntity, TObject>(Projection<TEntity, TObject> projection) 
        where TEntity : Entity
    {
        return QueryEvaluator.Evaluate(Set<TEntity>(), projection);
    }
}
```

### Как в итоге будет выглядеть обращение к базе в бизнес логике приложения
Рассмотрим кейс, в котором необходимо вернуть информацию о договоре `ContractResponse` с определенным именем (*по спецификации описанной в примере кода ранее `ContractByName`*).

*!Внимание: Данный пример показан в рамках чистой архитектуры и разделения запросов по подходу CQRS, что означает - что ваш вариант использования может выглядеть иначе, но суть использования библиотеки остается тот же.*
```csharp
// Класс запроса
public record ContractByNameQuery(string Name) : IQuery<ContractResponse?>;

// Обработчик запроса
public class ContractByNameQueryHandler : IQueryHandler<ContractByNameQuery, ContractResponse?>
{
    private readonly IDataContext _dataContext;

    public ContractByNameQueryHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    /// <inheritdoc />
    public Task<ContractResponse?> Handle(ContractByNameQuery request, CancellationToken cancellationToken = default)
    {
        return _dataContext.Get(new ContractByName(request.Name))
            .ProjectTo(new ContractToContractResponse())
            .FirstOrDefaultAsync(cancellationToken);
    }
}
```
## Дополнительные возможности:
1. Когда из контекста достается не проекция, а сущности, у методов `FirstOrDefault` и `ToArray` присутствует параметр `trackingType` который можно указать если изменения в сущностях, получаемых из контекста, имеет смысл не отслеживать. (*по умолчанию всегда отслеживаются*) 

    Пример:
    ```csharp
   var participant = await _context.Get(new ParticipantByFio(request.ParticipantFio))
                              .FirstOrDefaultAsync(trackingType: TrackingType.AsNoTracking, cancellationToken)
   ```
2. Спецификации можно объединять логическими операторами and - `&` и or - `|` для избежания дублирования логики спецификаций
    
    Пример:
    ```csharp
    // первая спецификация 
    public class ContractByName : Specification<Contract>
    {
        protected override Expression<Func<Contract, bool>> Criteria { get; }
        public ContractByName(string contractName)
        {
            Criteria = contract => contract.Name == contractName;
        }
    }
   
    // вторая спецификация
    public class ContractInThisMonth : Specification<Contract>
    {
        protected override Expression<Func<Contract, bool>> Criteria { get; }
        public ContractInThisMonth()
        {
            Criteria = contract => contract.CreationDate.Year == DateTime.UtcNow.Year &&
                                   contract.CreationDate.Month == DateTime.UtcNow.Month;
        }
    }
  
    // объединяющая спецификация
    public class ContractInThisMonthWithName : Specification<Contract>
    {
        protected override Expression<Func<Contract, bool>> Criteria { get; }
        public ContractInThisMonthWithName(string contractName)
        {
            Criteria = new ContractByName(contractName) & new ContractInThisMonth();
        }
    }
    ```
   
### Планируется реализовать:
1. Возможность в спецификации указать связанные сущности, которые необходимо вытянуть с основными (Include и ThenInclude)
2. OrderBy последовательности (как последовательности сущностей, так и их проекции)
3. Пагинация последовательности
