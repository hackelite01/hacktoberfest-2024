import React, { useEffect, useState } from "react";
import NewsItem from "./NewsItem";
import Spinner from "./Spinner";
import PropTypes from "prop-types";
import InfiniteScroll from "react-infinite-scroll-component";

const News = (props) => {
  const [articles, setArticles] = useState([]);
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [totalResults, setTotalResults] = useState(0);

  const capitalizeFirstLetter = (string) => {
    return string.charAt(0).toUpperCase() + string.slice(1);
  };
  const updateNews = async () => {
    props.setProgress(10);
    const url = `https://newsapi.org/v2/top-headlines?country=${props.country}&category=${props.category}&apiKey=${props.apikey}&page=${page}&pageSize=${props.pageSize}`;
    setLoading(true);
    let data = await fetch(url);
    props.setProgress(35);
    let parseData = await data.json();
    props.setProgress(67);

    setArticles(parseData.articles);
    setTotalResults(parseData.totalResults);
    setLoading(false);

    props.setProgress(100);
  };
  useEffect(() => {
    document.title = `${capitalizeFirstLetter(props.category)} - NewsPulse`;
    updateNews();
  }, []);
  
  const fetchMoreData = async () => {
    const url = `https://newsapi.org/v2/top-headlines?country=${
      props.country
    }&category=${props.category}&apiKey=${props.apikey}&page=${
      page + 1
    }&pageSize=${props.pageSize}`;
    setPage(page + 1);
    let data = await fetch(url);
    let parseData = await data.json();
    setArticles(articles.concat(parseData.articles));
    setTotalResults(parseData.totalResults);
  };
  if (!Array.isArray(articles)) {
    return null; 
  }
  return (
    <>
      <h2
        className="text-center"
        style={{ margin: " 35px 0px", marginTop: "80px" }}
      >
        NewsPulse - Top From {capitalizeFirstLetter(props.category)} Headlines
      </h2>
      {loading && <Spinner />}

      <InfiniteScroll
        dataLength={articles.length}
        next={fetchMoreData}
        hasMore={articles.length !== totalResults}
        loader={<Spinner />}
      >
        <div className="container">
          <div className="row">
            {articles.map((element) => {
              const title = element.title ? element.title.slice(0, 55) : "";
              const description = element.description
                ? element.description.slice(0, 139)
                : "";
              const imageUrl = element.urlToImage;
              const newsUrl = element.url;

              return (
                <div className="col-md-4" key={newsUrl}>
                  <NewsItem
                    title={title}
                    description={description}
                    imageUrl={imageUrl}
                    newsUrl={newsUrl}
                    author={element.author}
                    date={element.publishedAt}
                    source={element.source.name}
                  />
                </div>
              );
            })}
          </div>
        </div>
      </InfiniteScroll>
    </>
  );
};

News.defaultProps = {
  country: "us",
  pageSize: 8,
  category: "general",
};
News.propsTypes = {
  country: PropTypes.string,
  pageSize: PropTypes.number,
  category: PropTypes.string,
};

export default News;

