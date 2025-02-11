ymaps.ready(init);

function init() {

    // Стоимость за километр.
    var DELIVERY_TARIFF = 20,
        // Минимальная стоимость.
        MINIMUM_COST = 500,
        myMap = new ymaps.Map('map', {
            center: [56.845144, 60.587456],
            zoom: 9,
            controls: []
        }),
        // Создадим панель маршрутизации.
        routePanelControl = new ymaps.control.RoutePanel({
            options: {
                // Добавим заголовок панели.
                showHeader: true,
                title: 'Расчёт доставки'
            }
        }),
        zoomControl = new ymaps.control.ZoomControl({
            options: {
                size: 'small',
                float: 'none',
                position: {
                    bottom: 145,
                    right: 10
                }
            }
        });


    let location = '';

    // Пользователь сможет построить только автомобильный маршрут.
    routePanelControl.routePanel.options.set({
        types: { auto: true }
    });

    // Если вы хотите задать неизменяемую точку "откуда", раскомментируйте код ниже.
    routePanelControl.routePanel.state.set({
        fromEnabled: false,
        from: 'Свердловская область, Березовский, Кольцевая 5'
    });
    myMap.controls.add(routePanelControl).add(zoomControl);

    // Получим ссылку на маршрут.
    routePanelControl.routePanel.getRouteAsync().then(function (route) {

        // Зададим максимально допустимое число маршрутов, возвращаемых мультимаршрутизатором.
        route.model.setParams({ results: 1 }, true);

        // Повесим обработчик на событие построения маршрута.
        route.model.events.add('requestsuccess', function () {

            var activeRoute = route.getActiveRoute();
            if (activeRoute) {
                // Получим протяженность маршрута.
                let length = activeRoute.properties.get("distance"),
                    // Вычислим стоимость доставки.
                    price = calculate(Math.round(length.value / 1000));

                // Получим адреса "откуда" и "куда".
                let fromAddress = routePanelControl.routePanel.state.get('from'),
                    toAddress = routePanelControl.routePanel.state.get('to');
                // Создадим макет содержимого балуна маршрута.
                balloonContentLayout = ymaps.templateLayoutFactory.createClass(
                    '<span>Расстояние: ' + length.text + '.</span><br/>' +
                    '<span style="font-weight: bold; font-style: italic">Стоимость доставки: ' + price + ' р.</span>');
                // Зададим этот макет для содержимого балуна.
                route.options.set('routeBalloonContentLayout', balloonContentLayout);
                // Откроем балун.
                activeRoute.balloon.open();

                location = returnData(price, zoomControl.options.get('position'), fromAddress, toAddress);
                console.log(location);

            }
        });

    });
    // Функция, вычисляющая стоимость доставки.
    function calculate(routeLength) {
        return Math.max(routeLength * DELIVERY_TARIFF, MINIMUM_COST);
    }


    function returnData(price, zoomControlPosition, fromAddress, toAddress) {
        let stroka = `${toAddress}|${price}`;
        return stroka;
    }

    window.getLocation = () => {
        return location;
    }



}