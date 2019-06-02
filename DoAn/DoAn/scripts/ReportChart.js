var tmp = [];

function draw_chart() {
    am4core.useTheme(am4themes_animated);

    var chart = am4core.create("chartdiv", am4charts.XYChart);

    if (tmp.length > 0)
    {
        chart.data = tmp;
    }
   
    

    chart.padding(20, 20, 20, 20);

    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis.renderer.grid.template.location = 0;
    categoryAxis.dataFields.category = "month";
    categoryAxis.renderer.minGridDistance = 60;

    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

    var series = chart.series.push(new am4charts.ColumnSeries());
    series.dataFields.categoryX = "month";
    series.dataFields.valueY = "visitors";
    series.tooltipText = "{valueY.value}"
    series.columns.template.strokeOpacity = 0;

    chart.cursor = new am4charts.XYCursor();

    // as by default columns of the same series are of the same color, we add adapter which takes colors from chart.colors color set
    series.columns.template.adapter.add("fill", function (fill, target) {
        return chart.colors.getIndex(target.dataItem.index);
    });

}


function get_data_visit()
{
    $.ajax({
        url: '/Report/GetChartData',
        type: 'GET',
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            $.each(data, function (key, item) {
                tmp.push({ 'month': item.month, 'visitors': item.total_view});
            })
            draw_chart();
        }
    })
}


function get_data_profit()
{
    $.ajax({
        url: '/Report/GetDataProfit',
        type: 'GET',
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (data) {
            var list = '';
            $.each(data, function (key, item) {
                list += '<tr class="center-text">'
                + '<th>' + item.Item1 + '</th>'
                + '<th>' + item.Item2 + '</th>'
                + '</tr>';
            });
            $('#table_profit tbody').html(list);
            
        }
    })
}


