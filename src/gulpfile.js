/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    mainBowerFiles = require("main-bower-files");

var paths = {
    bower: "./bower_components/",
    weblib: "./wwwroot/lib/"
};

gulp.task("clean", function (callback) {
    rimraf(paths.weblib, callback);
});

gulp.task("copy", ["clean"], function () {
    var bowerFiles = mainBowerFiles({ checkExistence: true });
    //var bowerFiles = mainBowerFiles({ checkExistence: true, debugging: true });
    //console.log(bowerFiles);

    return gulp.src(bowerFiles, { base: paths.bower }).pipe(gulp.dest(paths.weblib));
});

