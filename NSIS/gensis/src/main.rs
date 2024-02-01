use std::fs;
use std::io::Write;
use std::path::{Path, PathBuf};

use clap::Parser;
use walkdir::WalkDir;

#[derive(Parser, Debug)]
#[command(author, version, about, long_about = None)]
struct Args {
    output_directory: PathBuf,
    target_directory: PathBuf,
}

fn main() {
    let args = Args::parse();

    print!("Generating install script...");
    std::io::stdout().flush().unwrap();

    let script_install_filepath = args.output_directory.join("generated/install_files.nsi");

    if !Path::exists(&script_install_filepath) {
        fs::create_dir_all(&args.output_directory).unwrap();
    }

    let mut install_script_file = fs::OpenOptions::new()
        .create(true)
        .write(true)
        .truncate(true)
        .open(&script_install_filepath)
        .unwrap();

    for entry in WalkDir::new(&args.target_directory).contents_first(false) {
        let entry = entry.unwrap();

        let relative_filepath = entry
            .path()
            .strip_prefix(PathBuf::from(&args.target_directory))
            .unwrap();

        // Ensures paths relative to $INSTDIR are preserved
        if let Some(parent) = relative_filepath.parent() {
            install_script_file
                .write_all(
                    format!("SetOutPath \"$INSTDIR\\{}\"\n", parent.to_str().unwrap()).as_bytes(),
                )
                .unwrap();
        } else {
            install_script_file
                .write_all("SetOutPath \"$INSTDIR\"\n".as_bytes())
                .unwrap();
        }

        // Directory structure is created automatically by NSIS
        if entry.path().is_file() {
            let line = format!("File \"{}\"\n", entry.path().display());
            install_script_file.write_all(line.as_bytes()).unwrap();
        }
    }

    println!("\tDone!");
    std::io::stdout().flush().unwrap();

    print!("Generating uninstall script...");
    std::io::stdout().flush().unwrap();

    let script_uninstall_filepath = args.output_directory.join("generated/uninstall_files.nsi");

    if !Path::exists(&script_uninstall_filepath) {
        fs::create_dir_all(&args.output_directory).unwrap();
    }

    let mut uninstall_script_file = fs::OpenOptions::new()
        .create(true)
        .write(true)
        .truncate(true)
        .open(script_uninstall_filepath)
        .unwrap();

    for entry in WalkDir::new(&args.target_directory).contents_first(true) {
        let entry = entry.unwrap();

        let relative_filepath = entry
            .path()
            .strip_prefix(PathBuf::from(&args.target_directory))
            .unwrap();

        let line = if entry.path().is_file() {
            format!("Delete \"$INSTDIR\\{}\"\n", relative_filepath.display())
        } else {
            format!("RMDir \"$INSTDIR\\{}\"\n", relative_filepath.display())
        };

        uninstall_script_file.write_all(line.as_bytes()).unwrap();
    }

    println!("\tDone!");
    std::io::stdout().flush().unwrap();

    println!("All scripts generated!");
}
