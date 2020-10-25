    /* initialize the calendar
        -----------------------------------------------------------------*/
    //Date for the calendar events (dummy data)
    var date = new Date()
    var d = date.getDate(),
        m = date.getMonth(),
        y = date.getFullYear()

    var Calendar = FullCalendar.Calendar;
    var Draggable = FullCalendar.Draggable;

    var calendarEl = document.getElementById('calendar');

    //    new Draggable(containerEl, {
    //        itemSelector: '.external-event',
    //        eventData: function(eventEl) {
    //        return {
    //        title: eventEl.innerText,
    //    backgroundColor: window.getComputedStyle( eventEl ,null).getPropertyValue('background-color'),
    //    borderColor: window.getComputedStyle( eventEl ,null).getPropertyValue('background-color'),
    //    textColor: window.getComputedStyle( eventEl ,null).getPropertyValue('color'),
    //    };
    //}
    //    });

    var calendar = new Calendar(calendarEl, {
        contentHeight: "auto",
        customButtons: {
            CreateButton: {
                text: 'Create Event',
                click: function () {
                    $("#CreateEventModal").modal('show')
                }
            }
        },
        headerToolbar: {
            left: 'prev,next today CreateButton',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        themeSystem: 'bootstrap',
        //Random default events
        editable: true,
        droppable: false, // this allows things to be dropped onto the calendar !!!
        drop: function (info) {
            info.draggedEl.parentNode.removeChild(info.draggedEl);
        },
        eventResize: function (info) {
            console.log("resized");
        },
        eventClick: function (info) {
            $("#UpdateEventInput").val(info.event.title);
            $('#UpdateEventTime').daterangepicker({
                timePicker: true,
                startDate: info.event.start,
                endDate:info.event.end,
                timePickerIncrement: 1,
                locale: {
                    format: 'MM/DD/YYYY HH:mm'
                }
            })
            $("#EditEventModal").modal("show");
            console.log(info.event.id);
            UpdateBtnClicked(info);
            DeleteBtnClicked(info);
        }
    });
function Calendar_initialize()
{
    GetEvent();
    calendar.render();
    // $('#calendar').fullCalendar()
}



/* initialize the external events
    -----------------------------------------------------------------*/
function ini_events(ele) {
    ele.each(function () {

        // create an Event Object (https://fullcalendar.io/docs/event-object)
        // it doesn't need to have a start or end
        var eventObject = {
            title: $.trim($(this).text()) // use the element's text as the event title
        }

        // store the Event Object in the DOM element so we can get to it later
        $(this).data('eventObject', eventObject)

        // make the event draggable using jQuery UI
        $(this).draggable({
            zIndex: 1070,
            revert: true, // will cause the event to go back to its
            revertDuration: 0  //  original position after the drag
        })

    })
}


function BindDatePicker() {
    $('#EventTime').daterangepicker({
        timePicker: true,
        timePickerIncrement: 1,
        locale: {
            format: 'MM/DD/YYYY HH:mm'
        }
    })
}

function GetEvent() {
    $.ajax({
        url: "https://localhost:5001/calendar/GetEventList",
        type: "GET",
        success: function (response) {
            console.log(response);
            $.each(response, function (i, data) {
                var events = {
                    id: data.id,
                    title: data.event,
                    start: Date.parse(moment(moment.utc(data.startTime).toDate())),
                    end: Date.parse(moment(moment.utc(data.endTime).toDate())),
                    backgroundColor: '#f39c12',
                    borderColor: '#f39c12'
                }
                calendar.addEvent(events);
            });
        }
    });
}

function CreateEventBtnClick() {
    $('#AddEventBtn').on('click', function () {
        var eventTitle = $('#InputEventTitle').val();
        let duration = $('#EventTime').val().split('-');
        var startTime = Date.parse(duration[0]);
        var endTime = Date.parse(duration[1]);
        var events = {
            title: eventTitle,
            start: startTime,
            end: endTime,
            backgroundColor: '#f39c12',
            borderColor: '#f39c12' 
        }
        calendar.addEvent(events);
        calendar.render();
        var payload = {
            Event: eventTitle,
            StartTime: moment.utc(startTime).format(),
            EndTime: moment.utc(endTime).format()
        }
        $.ajax({
            url: "https://localhost:5001/calendar/insertnewevent",
            type: 'POST',
            data: JSON.stringify(payload),
            contentType: "application/json; charset=utf-8",
            success: function () {
                calendar.removeAllEvents();
                GetEvent();
            }
        });
    })
}

function UpdateBtnClicked(info) {
    $("#UpdateEventBtn").unbind("click").click(function () {
        let duration = $('#UpdateEventTime').val().split('-');
        var startTime = Date.parse(duration[0]);
        var endTime = Date.parse(duration[1]);
        var payload = {
            Id: parseInt(info.event.id),
            Event: $('#UpdateEventInput').val(),
            StartTime: moment.utc(startTime).format(),
            EndTime: moment.utc(endTime).format()
        }
        $.ajax({
            url: "https://localhost:5001/calendar/UpdateEvent",
            type: 'PUT',
            data: JSON.stringify(payload),
            contentType: "application/json; charset=utf-8",
            success: function () {
                var event = calendar.getEventById(info.event.id)
                event.setProp('title', payload.Event);
                event.setStart(startTime);
                event.setEnd(endTime);
            }
        });
    });
}

function DeleteBtnClicked(info) {
    $("#DeleteEventBtn").unbind("click").click(function () {
        let duration = $('#UpdateEventTime').val().split('-');
        var startTime = Date.parse(duration[0]);
        var endTime = Date.parse(duration[1]);
        var payload = {
            Id: parseInt(info.event.id)
        }
        $.ajax({
            url: "https://localhost:5001/calendar/DeleteEvent",
            type: 'DELETE',
            data: JSON.stringify(payload),
            contentType: "application/json; charset=utf-8",
            success: function () {
                var event = calendar.getEventById(info.event.id)
                event.remove();
            }
        });
    });
}

ini_events($('#external-events div.external-event'));
Calendar_initialize()
BindDatePicker();
CreateEventBtnClick();

