for f in ./clean/*.csv; do
    docker cp "$f" mybus-database:/tmp/
done
