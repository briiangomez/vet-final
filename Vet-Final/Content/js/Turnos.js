$(document).ready(function () {
    $('#calendar').fullCalendar('destroy');
    $('#calendar').fullCalendar({
        locale: 'es',
        slotDuration: '00:30:00',
        contentHeight: 700,
        defaultDate: new Date(),
        minTime: '08:00:00',
        maxTime: '22:00:00',
        timeFormat: 'h:mm a',
        slotLabelFormat: 'h:mm a',
        handleWindowResize: true,
        header: {
            left: 'prev, next today',
            center: 'title',
            right: 'month, agendaWeek, agendaDay, listWeek'
        },

        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '/Turnos/ObtenerTurnos',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                       {
                           title: data.Title,
                           description: data.Desc,
                           url: data.url,
                           start: moment(data.Start_Date).format('YYYY-MM-DD HH:mm'),
                           end: moment(data.End_Date).format('YYYY-MM-DD HH:mm'),
                           backgroundColor: "#0069D9",
                           borderColor: "#EFF0F3"
                       });
                    });

                    callback(events);
                }
            });
        },
        eventClick: function (event) {
            if (event.url) {
                location.href = event.url;
                return false;
            }
        },
        eventRender: function (event, element) {
            //element.qtip(
            //{
            //    content: event.description
            //});
        },

        editable: false
    });

});
