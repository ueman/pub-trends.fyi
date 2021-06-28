const workerUrl = new URL("src/sqlite.worker.js", window.location);
const wasmUrl = new URL("src/sql-wasm.wasm", window.location);

function createChart(id, list, labels) {
  var ctx = document.getElementById(id).getContext('2d');
  var myChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: labels,
      datasets: [{
        label: id,
        data: list,
        backgroundColor: 'rgb(255, 99, 132)',
        borderColor: 'rgb(255, 99, 132)',
        borderWidth: 1,
      }]
    },
    options: {
      maintainAspectRatio: true,
      aspectRatio: 16/9,
      scales: {
        y: {
          beginAtZero: true
        }
      }
    }
  });
}

async function load(package) {
  const worker = await createDbWorker(
    [
      {
        from: "inline",
        config: {
          serverMode: "full",
          url: "/pub.db",
          requestChunkSize: 1024,
        },
      },
    ],
    workerUrl.toString(),
    wasmUrl.toString()
  );

  const result = await worker.db.query(`SELECT Likes, Popularity, Points, date(ReadAt) FROM Metrics WHERE PackageName = '${package}' ORDER BY ReadAt ASC`);

  console.log(result);

  if(result.length == 0) {
    return;
  }

  var likes = result.map(it => it.Likes);
  var popularity = result.map(it => it.Popularity * 100);
  var points = result.map(it => it.Points);
  var dates = result.map(it => it.ReadAt);

  createChart('likes', likes, dates);
  createChart('popularity', popularity, dates);
  createChart('points', points, dates);

  document.getElementById('stats').style.display = 'block';
  document.getElementById('placeholder').style.display = 'none';
}

function setTitle(package) {
  const header = document.getElementById('packageName');
  header.innerHTML = package;
}

const urlParams = new URLSearchParams(window.location.search);
const package = urlParams.get('p');

load(package);
setTitle(package);
