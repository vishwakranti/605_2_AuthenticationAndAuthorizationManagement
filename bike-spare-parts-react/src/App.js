import useFetch from "./functions/useFetch";
import Card from "react-bootstrap/Card";
import Container from "react-bootstrap/Container";

function App() {
  //debugger;
  const { data, loading, error } = useFetch("https://localhost:7004/api/stocksapi");

  if (error) {
    console.log(error);
  }
  return (
    <Container fluid>
      {loading && <div>Loading....{error}</div>}
      {data && (
        <div>
          {" Stock List  "}
          
          {data.map((item) => (
            <Card style={{ width: "28rem", padding: "10px" }}>
              <Card.Body>
                <hr></hr>
                <Card.Title style={{ fontWeight: "bold" }}>
                  {item.productName}
                </Card.Title>
                <Card.Subtitle className="mb-2 text-muted">
                  ${item.price}
                </Card.Subtitle>
                <Card.Text>{item.productDescription}</Card.Text>
              </Card.Body>
              <hr></hr>
            </Card>
          ))}
        </div>
      )}
    </Container>
  );
}
export default App;
