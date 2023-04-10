const gateway = "https://localhost:7078/api/hits";

var data = {
  siteId: meta.siteId,
  country: "",
  route: "/home/landing",
  CategoryId: meta.globalPageCategoryId,
};

async function getUserLocation() {
  const response = await fetch("http://geoplugin.net/json.gp", {
    method: "GET",
    referrerPolicy: "no-referrer",
  });

  const jsonData = await response.json();

  return jsonData;
}

document.querySelector("#send-request").addEventListener("click", async () => {
  const location = await getUserLocation();

  const hit = {
    ...data,
    country: location.geoplugin_countryName,
    route: window.location.pathname,
    deviceId: "Mohd12@!321",
  };

  await fetch(gateway, {
    method: "POST",
    referrerPolicy: "no-referrer",
    headers: {
      "Content-Type": "application/json",
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Credentials": "true",
      "Access-Control-Allow-Methods": "POST",
    },
    body: JSON.stringify(hit),
  });
});
